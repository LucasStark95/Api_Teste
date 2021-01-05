using Microsoft.AspNetCore.Mvc;
using NPagamento.Core.Responses;
using NPagamento.Core.Models;

namespace NPagamento.Api.Controllers
{
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        [HttpPost]
        [Route("api/pagamentos/compras")]
        public ActionResult<CompraResponse> AutorizaPagamento(Compra compra) 
            => new CompraResponse() 
            {   Valor = compra.valor, 
                Estado = compra.valor > 100 ? "APROVADO" : "REJEITADO" 
            };
    }
}
