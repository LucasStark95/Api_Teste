using NPista.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NPista.Core.Models
{
    public class Produto : EntityValidate, IEntity, IProduto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdeEstoque { get; set; }
        public double ValorUnitario { get; set; }

        public IList<Compra> Compras { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Nome))
                yield return new ValidationResult("O campo {0} é requerido", new[] { nameof(Nome) });
            if (QtdeEstoque < 0)
                yield return new ValidationResult("Valor inválido para o campo {0}", new[] { nameof(QtdeEstoque) });
            if (ValorUnitario < 0)
                yield return new ValidationResult("Valor inválido para o campo {0}", new[] { nameof(ValorUnitario) });

        }
    }
}
