using NPista.Core.Interfaces;


namespace NPista.Core.Requests
{
    public class CartaoRequest : ICartao
    {
        public string Titular { get; set; }
        public string Numero { get; set; }
        public string DataExpiracao { get; set; }
        public string bandeira { get; set; }
        public string Cvv { get; set; }
    }
}
