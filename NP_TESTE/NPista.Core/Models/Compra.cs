using NPista.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NPista.Core.Models
{
    public class Compra : EntityValidate, IEntity
    {
        public int Id { get; set; }

        public int CartaoId { get; set; }
        public Cartao Cartao { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int QtdeComprada { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (QtdeComprada < 0)
                yield return new ValidationResult("Valor inválido para o campo {0}", new[] { nameof(QtdeComprada) });
        }
    }
}
