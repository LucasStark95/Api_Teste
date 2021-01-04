using NPista.Core.Models;
using NPista.Data.EFCore.Context;

namespace NPista.Data.EFCore.Repositorios
{
    public class CompraRepositorio : RepositorioBase<Compra>
    {
        public CompraRepositorio(Contexto contexto) : base(contexto) { }
    }
}
