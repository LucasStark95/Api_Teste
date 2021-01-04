using NPista.Core.Interfaces;
using System;

namespace NPista.Core.Responses
{
    public class ProdutoResponse :IEntity, IProduto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdeEstoque { get; set; }
        public double ValorUnitario { get; set; }
        public DateTime DataUltimaCompra { get; set; }
        public double ValorUltimaCompra { get; set; }

    }
}
