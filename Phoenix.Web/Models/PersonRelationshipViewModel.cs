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
    public class PersonRelationshipViewModel : IValidatableObject
    {
        public int ID { get; set; }
        public int PersonId { get; set; }
        public int RelationWithPersonId { get; set; }
        public string RelationshipName { get; set; }
        public string RelationshipTypeName { get; set; }
        public int RelationshipTypeId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new PersonRelationshipViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage,
                new[] { item.PropertyName }));
        }
    }
}