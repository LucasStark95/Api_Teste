using NPagamento.Core.Interfaces;

namespace NPagamento.Core.Models
{
    public class Compra : ICompra
    {
        public double valor { get; set; }
        public Cartao Cartao { get; set; }
    }
}
