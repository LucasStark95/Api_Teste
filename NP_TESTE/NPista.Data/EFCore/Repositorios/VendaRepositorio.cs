using NPista.Core.Models;
using NPista.Data.EFCore.Context;

namespace NPista.Data.EFCore.Repositorios
{
    /// <summary>
    /// Venda Repositório.
    /// </summary>
    public class VendaRepositorio : RepositorioBase<Venda>
    {
        public VendaRepositorio(Contexto contexto) : base(contexto) { }
    }
}
