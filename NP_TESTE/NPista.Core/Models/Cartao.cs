using NPista.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NPista.Core.Models
{
    public class Cartao : EntityValidate,  IEntity, ICartao
    {
        public int Id { get; set; }
        public string Titular { get; set; }
        public string Numero { get; set; }
        public string DataExpiracao { get; set; }
        public string bandeira { get; set; }
        public string Cvv { get; set; }

        public IList<Compra> Compras { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Titular))
                yield return new ValidationResult("O campo {0} é requerido", new[] { nameof(Titular) });
            if (string.IsNullOrWhiteSpace(Numero))
                yield return new ValidationResult("O campo {0} é requerido", new[] { nameof(Numero) });
            if (Numero.Length < 13 || Numero.Length > 16)
                yield return new ValidationResult("Valor inválido para o campo {0}", new[] { nameof(Numero) });
        }
    }
}
