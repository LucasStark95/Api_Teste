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
    /// <summary>
    /// API Pagamento Utils.
    /// Classe responsável pelo consumo da API Pagamento.
    /// </summary>
    public class ApiPagamentoUtils
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        
        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="httpClient"></param>
        public ApiPagamentoUtils(AppSettings settings, IHttpClientFactory httpClient)
        {
            _appSettings = settings;
            _httpClient = httpClient.CreateClient();
        }
        /// <summary>
        /// Cria e envia a requisição para API.
        /// </summary>
        /// <example>
        ///
        ///  { "Valor": 100, "Cartao": {  "Titular" : "Antonio Lucas de Almeida","Numero": "1234567891012", DataExpiracao": "12/2018", "Bandeira": "Master", "Cvv": "145" }}
        /// 
        /// </example>
        /// <param name="compra"></param>
        /// <returns>
        ///  Retorna se foi autorizado ou não a realização da venda.
        /// </returns>
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

                    await response;

                    return result;
                }
                catch
                {
                    throw new Exception();
                }
            
            }
        }
}
