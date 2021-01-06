using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPista.Data.EFCore.Repositorios;
using NPista.Core.Models;
using NPista.Core.Responses;

namespace NPista.API.Controllers
{
    /// <summary>
    /// Produto Controller.
    /// Classe responsável pelo recebimento das requisições referente ao produto.
    /// </summary>
    [Route("api/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="produtoRepositorio"></param>
        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Retorna um produto detalhado especifico por meio do seu identificador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoResponse>> GetProdutoDetails(int id)
        {
            try
            {
                var produtoDB = await _produtoRepositorio.GetProdutoDetailsByIdAsync(id);
                return new JsonResult(produtoDB);
            }
            catch
            {
                return BadRequest("Ocorreu um erro desconhecido.");
            }
        }

        /// <summary>
        /// Armazenar um produto da base de dados.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Remove um produto da base de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
