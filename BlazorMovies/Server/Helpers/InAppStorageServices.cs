using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Helpers
{
    //criaremos aqui todo o processo para o salvamento local
    public class InAppStorageServices : IFileStorageService
    {
        //Esses dois caras são:
        //Ela tem uma infomação importante webRootPath, para o salvamento local
        //apartir do servicdor web não o IS, ela fornece i caminho fisico dentro do servidor web onde esta o www.root
        //Nos não sabemos onde vai estar o cara, que hospedar o servidor vai colocar onde quiser
        private readonly IWebHostEnvironment env;
        //Esse é um contexto do HTTP, que vai saber o que está sendo mandado para mim
        private readonly IHttpContextAccessor httpContextAccessor;

        public InAppStorageServices(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        //Precisamos de três intems (nova imagem, editar ou substituir) a ordem independe
        //Delete é uma thread não assincorna, que roda fora do codigo mas eu aguardo ela
        public Task DeleteFile(string fileRoute, string containerName)
        {
            //fileroute é o filePath, basicamente o path do arquivo
            string fileName = Path.GetFileName(fileRoute);//separando o caminho do nome do arquivo
            //webRootPath é o local fisico do disco do servidor web
            //containerName é o folder, pasta ou diretorio
            string fileDirectory = Path.Combine(env.WebRootPath, containerName, fileName);
            if (File.Exists(fileDirectory))//Caminho completo
            {
                File.Delete(fileDirectory);
            }
            return Task.FromResult(0);//Retorno simples Ok
            //aqui poderia ser feito um melhor tratamento, arquivo em loq
        }
        //content array de byte
        public async Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute)
        {
            if (!string.IsNullOrEmpty(fileRoute))
            {
                await DeleteFile(fileRoute, containerName);
            }
            //salvar
            return await SaveFile(content, extension, containerName);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string containerName)
        {
            //Guid faz a geração de um Identificador unico, para não sobrepor um arquivo!
            var fileName = $"{Guid.NewGuid()}.{extension}";
            string folder = Path.Combine(env.WebRootPath, containerName);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string savingPath = Path.Combine(folder, fileName);
            await File.WriteAllBytesAsync(savingPath, content);
            //aqui eu tenho um problema, preciso generalizar isso. Devido a possiveis mudanças.
            //Pegando a URL atual agora do contexto
            var currentUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var pathForDatabase = Path.Combine(currentUrl, containerName, fileName);
            return pathForDatabase;//Retorna o que efetivamente é salvo no banco
        }
    }
}
