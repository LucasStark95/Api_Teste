using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NPista.API.Utils;
using NPista.Core.Models;
using NPista.Core.Requests;
using NPista.Data.EFCore.Repositorios;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NPista.API.Controllers
{
    /// <summary>
    /// Compra COntroller.
    /// Controladora responsável as receber e manipuluas requisições,
    /// referentes as compras dos produtos.
    /// </summary>
    [Route("api/compras")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly VendaRepositorio _compraRepositorio;
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly ApiPagamentoUtils _apiPagamentoService;
        /// <summary>
        /// Construtor da Classe
        /// </summary>
        /// <param name="compraRepositorio"></param>
        /// <param name="produtoRepositorio"></param>
        /// <param name="settings"></param>
        /// <param name="httpClient"></param>
        public CompraController(VendaRepositorio compraRepositorio, 
            ProdutoRepositorio produtoRepositorio, IOptions<AppSettings> settings, 
            IHttpClientFactory httpClient)
        {
            _compraRepositorio = compraRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _apiPagamentoService = new ApiPagamentoUtils(settings.Value, httpClient);
        }

        /// <summary>
        /// Cria e salva uma venda
        /// </summary>
        /// <param name="venda"></param>
        /// /// <returns>
        /// Mensagem de sucesso na operação
        /// </returns>
        /// <example>
        /// 
        /// { "Valor": 100,  "Cartao": { "Titular" : "Antonio Lucas de Almeida", "Numero": "1234567891012", "DataExpiracao": "12/2018", "Bandeira": "Master", "Cvv": "145" }}
        /// 
        /// </example>
        // POST api/compras
        [HttpPost]
            public async Task<ActionResult<Venda>> CreateCompra(Venda venda)
            {
                try
                {
                    var produtoDB = await _produtoRepositorio.GetProdutoByIdAsync(venda.ProdutoId);
                    var valor = CalculaValor(produtoDB.ValorUnitario, venda.QtdeComprada);

                    var authorization = await VerifyAuthorization(venda.Cartao, valor);

                    if (!authorization) throw new Exception();

                    var compraDB = await _compraRepositorio.AdicionarAsync(venda);
                    await _produtoRepositorio.BaixaEstoqueByIdAsync(venda.ProdutoId, venda.QtdeComprada);
               
                    return Ok("Venda realizada com sucesso.");
                }
                catch
                {
                    return BadRequest("Ocorreu um erro desconhecido.");
                }
            }

            /// <summary>
            /// Calcula o valor da última venda
            /// </summary>
            /// <param name="valor"></param>
            /// <param name="quantidade"></param>
            /// <returns></returns>
            private double CalculaValor(double valor, int quantidade) => valor * quantidade;

            /// <summary>
            /// Consulta se é autorizado realizar a venda ou não.
            /// </summary>
            /// <param name="cartao"></param>
            /// <param name="valor"></param>
            /// <returns></returns>
            private async Task<bool> VerifyAuthorization(Cartao cartao, double valor)
            {
                var request = new CompraRequest
                {
                    Valor = valor,
                    Cartao = ConvertObj(cartao)
                };

                var result = await _apiPagamentoService.GetAuthorizationCompra(request);

                return result.Estado.Contains("APROVADO");
            }
            
            /// <summary>
            /// Serializa um objeto no tipo compatível.
            /// </summary>
            /// <param name="cartao"></param>
            /// <returns></returns>
            private CartaoRequest ConvertObj(Cartao cartao)
            {
                return JsonConvert.DeserializeObject<CartaoRequest>(JsonConvert.SerializeObject(cartao));
            }
        }
}
