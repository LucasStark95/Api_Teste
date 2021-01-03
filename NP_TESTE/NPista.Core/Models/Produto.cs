using NPista.Core.Models.Interfaces;

namespace NPista.Core.Models
{
    public class Produto : IEntity, IProduto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdeEstoque { get; set; }
        public double ValorUnitario { get; set; }
    }
}
