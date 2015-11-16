using FluentValidation;
using Phoenix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Web.Infrastructure.Validators
{
    public class DiagnosisViewModelValidator : AbstractValidator<DiagnosisViewModel>
    {
        public DiagnosisViewModelValidator()
        {
            RuleFor(diagnosis => diagnosis.Name).NotEmpty()
                .Length(1, 100).WithMessage("Diagnosis name must be between 1 - 100 characters");
        }
    }
}