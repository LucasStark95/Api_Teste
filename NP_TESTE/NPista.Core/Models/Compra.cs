using NPista.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace NPista.Core.Models
{
    public class Compra : IEntity
    {
        public int Id { get; set; }
        public int CartaoId { get; set; }
        [Required]
        public Cartao Cartao { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        [Range(0,int.MaxValue)]
        public int QtdeComprada { get; set; }
        public DateTime DataCompra { get; set; }
    }
}
