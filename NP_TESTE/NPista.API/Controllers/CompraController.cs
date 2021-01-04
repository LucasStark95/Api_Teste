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

        public CompraController(CompraRepositorio compraRepositorio)
        {
            _compraRepositorio = compraRepositorio;
        }

        // POST api/compras
        [HttpPost]
        public async Task<ActionResult<Compra>> CreateProduto(Compra compra)
        {
            try
            {
                var compraDB = await _compraRepositorio.AdicionarAsync(compra);

                if (compraDB != null)
                    return Ok();
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest("Ocorreu um erro desconhecido.");
            }
        }
    }
}
