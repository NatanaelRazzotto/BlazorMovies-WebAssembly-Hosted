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
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public PersonController(ApplicationDbContext context)
        {
            this.context = context;
        }
        //implementação dos endPoits
        //Aqui teremos o crud
        //Para gravar
        [HttpPost] // O retorno é de acordo igual o especificado no projeto
        public async Task<ActionResult<int>> Post(Person person)
        {
            context.Add(person);
            await context.SaveChangesAsync();
            return person.Id;
        }
         
    }
}
