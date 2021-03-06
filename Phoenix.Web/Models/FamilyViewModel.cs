﻿using Phoenix.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Web.Models
{
    [Bind(Exclude = "UniqueKey")]
    public class FamilyViewModel : IValidatableObject
    {
        public int ID { get; set; }
        public string FamilyName { get; set; }
        public DateTime FirstRegisteredDate { get; set; }
        public string Notes { get; set; }
        public string Ethnicity { get; set; }
        public int EthnicityID { get; set; }
        public string Diagnosis { get; set; }
        public int? DiagnosisID { get; set; }
        public string DiagnosisSubType { get; set; }
        public int? DiagnosisSubTypeID { get; set; }
        public bool Deleted { get; set; }
        public int Persons { get; set; }
        public string FamilyIdentifier { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new FamilyViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage,
                new[] { item.PropertyName }));
        }
    }
}