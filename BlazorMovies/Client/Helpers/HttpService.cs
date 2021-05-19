using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers
{
    //Implementação da interface -IHttpService
    public class HttpService : IHTTPService
    {
        private readonly HttpClient httpClient;

        public HttpService(HttpClient httpClient) {
            this.httpClient = httpClient;
        }
        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T data)
        {
            //Não estamos usando o NewTonSoft apenas o .net Puro
            var dataJson = JsonSerializer.Serialize(data);
            var strigContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, strigContent);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }
    }
}
