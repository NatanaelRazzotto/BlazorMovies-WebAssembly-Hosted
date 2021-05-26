using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Controllers
{
    //Temos um padrão para todas as controller por isso usamos o "ControllerBase"
    [ApiController] //Temos de definir
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IFileStorageService fileStorageService;
        public PeopleController(ApplicationDbContext context, IFileStorageService fileStorageService)
        {
            this.context = context;
            this.fileStorageService = fileStorageService;
        }
        //implementação dos endPoits
        //Aqui teremos o crud
        //Para gravar
        [HttpPost] // O retorno é de acordo igual o especificado no projeto
        public async Task<ActionResult<int>> Post(Person person)
        {
            if (!string.IsNullOrWhiteSpace(person.Picture))
            {
                var persorPicture = Convert.FromBase64String(person.Picture);
                person.Picture = await fileStorageService.SaveFile(persorPicture, ".jpg", "people");
            }
            context.Add(person);
            await context.SaveChangesAsync();
            return person.Id;
        }
         
    }
}
