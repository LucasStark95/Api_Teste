namespace NPista.Core.Models.Interfaces
{
    public interface IProduto
    {
        public string Nome { get; set; }
        public int QtdeEstoque { get; set; }
        public double ValorUnitario { get; set; }
    }

}
