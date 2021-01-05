using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPista.Data.EFCore.Repositorios;
using NPista.Core.Models;
using NPista.Core.Responses;

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
                return new JsonResult(produtos.ToList());
            }
            catch 
            {
                return BadRequest("Ocorreu um erro desconhecido.");
            }
        }

        // GET api/produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoResponse>> Get(int id)
        {
            try
            {
                var produtoDB = await _produtoRepositorio.GetProdutoResponseByIdAsync(id);
                return new JsonResult(produtoDB);
            }
            catch
            {
                return BadRequest("Ocorreu um erro desconhecido.");
            }
        }

        // POST api/produtos
        [HttpPost]
        public async Task<ActionResult> CreateProduto(Produto produto)
        {
            try
            {
                var produtoDB = await _produtoRepositorio.AdicionarAsync(produto);
                return Ok("Produto Cadastrado");
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
