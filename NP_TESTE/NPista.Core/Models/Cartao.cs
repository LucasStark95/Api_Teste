using NPista.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NPista.Core.Models
{
    public class Cartao :  IEntity, ICartao
    {
        public int Id { get; set; }
        [Required]
        public string Titular { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(13)]
        public string Numero { get; set; }
        public string DataExpiracao { get; set; }
        public string Bandeira { get; set; }
        public string Cvv { get; set; }

        public IList<Venda> Compras { get; set; }
    }
}
