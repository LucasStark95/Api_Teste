namespace NPagamento.Core.Interfaces
{
    public interface ICartao
    {
        public string Titular { get; set; }
        public string Numero { get; set; }
        public string DataExpiracao { get; set; }
        public string Bandeira { get; set; }
        public string Cvv { get; set; }
    }
}
