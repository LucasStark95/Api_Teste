namespace NPista.Core.Models.Interfaces
{
    public interface ICartao
    {
        public string Titular { get; set; }
        public string Numero { get; set; }
        public string DataExpiracao { get; set; }
        public string bandeira { get; set; }
        public string Cvv { get; set; }

    }
}
