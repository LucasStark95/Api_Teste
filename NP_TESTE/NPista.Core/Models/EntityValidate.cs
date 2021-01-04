using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NPista.Core.Models
{
    public class EntityValidate : IValidatableObject
    {
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results, true);
            return results;
        }
    }
}
