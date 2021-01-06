using Microsoft.EntityFrameworkCore;
using NPista.Core.Models;
using NPista.Core.Responses;
using NPista.Data.EFCore.Context;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace NPista.Data.EFCore.Repositorios
{
    /// <summary>
    /// Produto Repositório.
    /// </summary>
    public class ProdutoRepositorio : RepositorioBase<Produto>
    {
        public ProdutoRepositorio(Contexto contexto) : base(contexto) { }
        
        /// <summary>
        /// Busca o produto detalhado na base de dados.
        /// </summary>
        /// <param name="id">Id do Produto</param>
        /// <returns></returns>
        public async Task<ProdutoResponse> GetProdutoDetailsByIdAsync(int id) 
        {
            var produto = await GetProdutoByIdAsync(id);

            if (produto == null) throw new NullReferenceException();

            var lastSale = GetLastSale(produto);
            
            return new ProdutoResponse 
            { 
                Id = produto.Id,
                DataUltimaCompra = lastSale?.DataCompra,
                Nome = produto.Nome,
                QtdeEstoque = produto.QtdeEstoque,
                ValorUnitario = produto.ValorUnitario,
                ValorUltimaCompra = lastSale != null ? lastSale.QtdeComprada * produto.ValorUnitario : 0,
            };
        }

        /// <summary>
        /// Retorna todos os produtos armazenados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Produto>> GetAllProdutosAsync() => await BuscarTodos().ToListAsync();

        /// <summary>
        /// Exclui o registro do produto na base de dadps.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task ExcluirProduto(int id)
        {
            await ExcluirAsync(p => p.Id == id);
        }

        /// <summary>
        /// Dar baixa no estoque de determinado produto pelo identificador.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public async Task BaixaEstoqueByIdAsync(int id, int valor)
        {
            var produto = await GetProdutoByIdAsync(id);

            if (produto == null || produto.QtdeEstoque - valor < 0) throw new NullReferenceException();

            produto.QtdeEstoque -= valor;

            await AtualizarAsync(produto);
        }

        /// <summary>
        /// Busca um produto pelo seu identificador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            var result = await Buscar(p => p.Id == id)
                .Include(p => p.Vendas).FirstOrDefaultAsync();

            if (result == null) throw new NullReferenceException();

            return result;
        }

        private Venda GetLastSale(Produto produto)
        {
            return produto?.Vendas?.OrderByDescending(o => o.DataCompra).SingleOrDefault();
        }
    }
}
