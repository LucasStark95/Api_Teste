using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPista.Data.EFCore.Repositorios;
using NPista.Core.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NPista.API.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        // GET: api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAllProdutos()
        {
            try
            {
                var produtos = await _produtoRepositorio.GetAllProdutosAsync();
                return produtos.ToList();
            }
            catch 
            {
                return BadRequest(400);
            }
        }

        // GET api/produtos/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/produtos
        [HttpPost]
        public async Task<ActionResult<Produto>> CreateProduto(Produto produto)
        {
            try
            {
                var produtoDB = await _produtoRepositorio.AdicionarAsync(produto);

                if (produtoDB != null)
                    return Ok();
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest("Ocorreu um erro desconhecido.");
            }
        }

        // DELETE api/produtos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduto(int id)
        {
            try
            {
                await _produtoRepositorio.ExcluirProduto(id);
                return Ok("Produto excluído com sucesso.");
            }   
            catch
            {
                return BadRequest("Ocorreu um erro desconhecido.");
            }
        }
    }
}
