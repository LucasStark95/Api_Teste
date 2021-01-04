namespace NPista.Core.Requests
{
    public class CompraRequest
    {
        public double Valor { get; set; }
        public CartaoRequest Cartao { get; set; }
    }
}
