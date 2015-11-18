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
    public class PersonViewModel : IValidatableObject
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Twin { get; set; }
        public bool Adopted { get; set; }
        public decimal HeightCM { get; set; }
        public decimal WeightKG { get; set; }
        public bool Deceased { get; set; }
        public DateTime FirstRegisteredDate { get; set; }
        public string Notes { get; set; }
        public string Ethnicity { get; set; }
        public int EthnicityID { get; set; }
        public string Diagnosis { get; set; }
        public int DiagnosisID { get; set; }
        public string DiagnosisSubType { get; set; }
        public int DiagnosisSubTypeID { get; set; }
        public int FamilyID { get; set; }
        public string FamilyName { get; set; }
        public bool Deleted { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new PersonViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage,
                new[] { item.PropertyName }));
        }
    }
}