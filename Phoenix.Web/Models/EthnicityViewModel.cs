using Phoenix.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Web.Models
{
    [Bind(Exclude = "UniqueKey")]
    public class EthnicityViewModel : IValidatableObject
    {
        public int ID { get; set; }
        public string EthnicityName { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new EthnicityViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage,
                new[] { item.PropertyName }));
        }
    }
}