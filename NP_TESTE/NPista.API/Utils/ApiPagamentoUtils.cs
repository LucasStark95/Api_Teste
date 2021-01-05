using Microsoft.Extensions.Options;
using NPista.Core.Models;
using NPista.Core.Requests;
using NPista.Core.Responses;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NPista.API.Utils
{
    public class ApiPagamentoUtils
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;

        public ApiPagamentoUtils(AppSettings settings, IHttpClientFactory httpClient)
        {
            _appSettings = settings;
            _httpClient = httpClient.CreateClient();
        }

        public async Task<CompraResponse> GetAuthorizationCompra(CompraRequest compra)
        {
            try
            {
                CompraResponse result = null;
                var content = new StringContent(JsonSerializer.Serialize(compra), Encoding.UTF8, "application/json");
                var response = _httpClient.PostAsync(_appSettings.UrlApiPagamento, content).ContinueWith(task => 
                {
                    var r = task.Result;
                    var jsonString = r.Content.ReadAsStringAsync();
                    jsonString.Wait();
                        
                    result = JsonSerializer.Deserialize<CompraResponse>(jsonString.Result, 
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
 
                });

                response.Wait();

                return result;
            }
            catch
            {
                throw new Exception();
            }
            
        }
    }
}
