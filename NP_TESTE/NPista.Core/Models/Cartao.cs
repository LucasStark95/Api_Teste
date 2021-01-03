using NPista.Core.Models.Interfaces;
using System.Collections.Generic;

namespace NPista.Core.Models
{
    public class Cartao : IEntity, ICartao
    {
        public int Id { get; set; }
        public string Titular { get; set; }
        public string Numero { get; set; }
        public string DataExpiracao { get; set; }
        public string bandeira { get; set; }
        public string Cvv { get; set; }

        public IList<Compra> Compras { get; set; }
    }
}
