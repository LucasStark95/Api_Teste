using NPagamento.Core.Models;

namespace NPagamento.Core.Interfaces
{
    public interface ICompra
    {
        public double valor { get; set; }
        public Cartao Cartao { get; set; }
    }
}
