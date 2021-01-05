using Microsoft.AspNetCore.Mvc;
using NPista.Core.Models;
using NPista.Data.EFCore.Repositorios;
using System.Threading.Tasks;

namespace NPista.API.Controllers
{
    [Route("api/compras")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly CompraRepositorio _compraRepositorio;
        private readonly ProdutoRepositorio _produtoRepositorio;

        public CompraController(CompraRepositorio compraRepositorio, ProdutoRepositorio produtoRepositorio)
        {
            _compraRepositorio = compraRepositorio;
            _produtoRepositorio = produtoRepositorio;
        }

        // POST api/compras
        [HttpPost]
        public async Task<ActionResult<Compra>> CreateCompra(Compra compra)
        {
            try
            {
                var compraDB = await _compraRepositorio.AdicionarAsync(compra);
                await _produtoRepositorio.BaixaEstoqueByIdAsync(compra.ProdutoId, compra.QtdeComprada);
               
                return Ok("Venda realizada com sucesso.");
            }
            catch
            {
                return BadRequest("Ocorreu um erro desconhecido.");
            }
        }
    }
}
