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
    [Route("api/compras")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly CompraRepositorio _compraRepositorio;
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly ApiPagamentoUtils _apiPagamentoService;

        public CompraController(CompraRepositorio compraRepositorio, 
            ProdutoRepositorio produtoRepositorio, IOptions<AppSettings> settings, 
            IHttpClientFactory httpClient)
        {
            _compraRepositorio = compraRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _apiPagamentoService = new ApiPagamentoUtils(settings.Value, httpClient);
        }

        // POST api/compras
        [HttpPost]
        public async Task<ActionResult<Compra>> CreateCompra(Compra compra)
        {
            try
            {
                var produtoDB = await _produtoRepositorio.GetProdutoByIdAsync(compra.ProdutoId);
                var valor = CalculaValor(produtoDB.ValorUnitario, compra.QtdeComprada);

                var authorization = await VerifyAuthorization(compra.Cartao, valor);

                if (!authorization) throw new Exception();

                var compraDB = await _compraRepositorio.AdicionarAsync(compra);
                await _produtoRepositorio.BaixaEstoqueByIdAsync(compra.ProdutoId, compra.QtdeComprada);
               
                return Ok("Venda realizada com sucesso.");
            }
            catch
            {
                return BadRequest("Ocorreu um erro desconhecido.");
            }
        }

        private double CalculaValor(double valor, int quantidade) => valor * quantidade;

        private async Task<bool> VerifyAuthorization(Cartao cartao, double valor)
        {
            var request = new CompraRequest();
            request.Valor = valor;
            request.Cartao = ConvertObj(cartao);

            var result = await _apiPagamentoService.GetAuthorizationCompra(request);

            return result.Estado.Contains("APROVADO");
        }

        private CartaoRequest ConvertObj(Cartao cartao)
        {
            return JsonConvert.DeserializeObject<CartaoRequest>(JsonConvert.SerializeObject(cartao));
        }
    }
}
