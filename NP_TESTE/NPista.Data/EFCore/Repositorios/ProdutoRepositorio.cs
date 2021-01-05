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
    public class ProdutoRepositorio : RepositorioBase<Produto>
    {
        public ProdutoRepositorio(Contexto contexto) : base(contexto) { }

        public async Task<ProdutoResponse> GetProdutoResponseByIdAsync(int id) 
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

        public async Task<IEnumerable<Produto>> GetAllProdutosAsync() => await BuscarTodos().ToListAsync();

        public async Task ExcluirProduto(int id)
        {
            await ExcluirAsync(p => p.Id == id);
        }

        public async Task BaixaEstoqueByIdAsync(int id, int valor)
        {
            var produto = await GetProdutoByIdAsync(id);

            if (produto == null || produto.QtdeEstoque - valor < 0) throw new NullReferenceException();

            produto.QtdeEstoque -= valor;

            await AtualizarAsync(produto);
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            var result = await Buscar(p => p.Id == id)
                .Include(p => p.Compras).FirstOrDefaultAsync();

            if (result == null) throw new NullReferenceException();

            return result;
        }

        private Compra GetLastSale(Produto produto)
        {
            return produto?.Compras?.OrderByDescending(o => o.DataCompra).SingleOrDefault();
        }
    }
}
