using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers
{
    public interface IHTTPService
    {
        Task<HttpResponseWrapper<object>> Post<T>(string url, T data);
    }
}
