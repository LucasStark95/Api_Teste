using Microsoft.EntityFrameworkCore;
using NPista.Core.Models;
using NPista.Data.EFCore.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPista.Data.EFCore.Repositorios
{
    public class ProdutoRepositorio : RepositorioBase<Produto>
    {
        public ProdutoRepositorio(Contexto contexto) : base(contexto) { }

        public async Task<Produto> GetProdutoByIdAsync(int id) 
        {
            return await Buscar(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Produto>> GetAllProdutosAsync() => await BuscarTodos().ToListAsync();

        public async Task ExcluirProduto(int id)
        {
            await ExcluirAsync(p => p.Id == id);
        }
    }
}
