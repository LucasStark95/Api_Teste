using NPista.Core.Models.Interfaces;
using System.Collections.Generic;

namespace NPista.Core.Models
{
    public class Produto : IEntity, IProduto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdeEstoque { get; set; }
        public double ValorUnitario { get; set; }

        public IList<Compra> Compras { get; set; }
    }
}
