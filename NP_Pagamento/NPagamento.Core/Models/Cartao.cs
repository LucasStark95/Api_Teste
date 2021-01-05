using NPagamento.Core.Interfaces;

namespace NPagamento.Core.Models
{
    public class Cartao : ICartao
    {
        public string Titular { get; set; }
        public string Numero { get; set; }
        public string DataExpiracao { get; set; }
        public string Bandeira { get; set; }
        public string Cvv { get; set; }
    }
}
