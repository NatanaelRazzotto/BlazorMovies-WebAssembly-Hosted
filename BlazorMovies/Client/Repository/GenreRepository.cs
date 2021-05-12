using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IHTTPService httpService;
        private readonly string url = "api/genres";

        public GenreRepository(IHTTPService httpService) {
            this.httpService = httpService;
        }
        public async Task CreateGenre(Genre genre)
        {
            var reponse = await httpService.Post(url, genre);
            if (!reponse.Sucess)
            {
                throw new ApplicationException(await reponse.GetBody());
            }
        }

    }
}
