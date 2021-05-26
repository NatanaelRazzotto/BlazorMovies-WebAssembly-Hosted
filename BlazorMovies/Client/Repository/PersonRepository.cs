using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IHTTPService httpService;
        //A controller la no servidor, dizemos o caminho
        private readonly string url = "api/people";

        public PersonRepository(IHTTPService httpService)
        {
            this.httpService = httpService;
        }
        //Task que fara a construção para a gente

        public async Task CreatePerson(Person person)
        {
            var response = await httpService.Post(url, person);
            if (!response.Sucess)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
