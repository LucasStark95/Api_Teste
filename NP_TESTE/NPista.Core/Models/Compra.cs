using NPista.Core.Interfaces;

namespace NPista.Core.Models
{
    public class Compra : IEntity
    {
        public int Id { get; set; }

        public int CartaoId { get; set; }
        public Cartao Cartao { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int QtdeComprada { get; set; }
    }
}
