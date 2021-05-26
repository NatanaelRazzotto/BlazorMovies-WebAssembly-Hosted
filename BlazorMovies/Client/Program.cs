using BlazorMovies.Client.Helpers;
using BlazorMovies.Client.Repository;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMovies.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRepository, RepositoryInMemory>();

            //Aqui vai variar do que a gente tiver trabalhando, ele n�o � transiente mans tamb�mn�o 
            //temos a terceira situa��o

            //a priori um s�
            services.AddScoped<IHTTPService, HttpService>();
            //Para cada conjunto MVCvoc�vai ter uma entrada para os servi�os scoped
            //um para cada conjunto controller, interface, implementa���o de interface de cada  entidade
            services.AddScoped<IGenreRepository, GenreRepository>();
            //Criando servico
            services.AddScoped<IPersonRepository, PersonRepository>();
        }
    }
}
