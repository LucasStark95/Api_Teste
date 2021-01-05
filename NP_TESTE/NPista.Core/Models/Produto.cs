using NPista.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NPista.Core.Models
{
    public class Produto :  IEntity, IProduto
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int QtdeEstoque { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public double ValorUnitario { get; set; }
        public IList<Compra> Compras { get; set; }
    }
}
