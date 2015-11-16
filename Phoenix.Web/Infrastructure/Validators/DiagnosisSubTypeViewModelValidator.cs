using FluentValidation;
using Phoenix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Web.Infrastructure.Validators
{
    public class DiagnosisSubTypeViewModelValidator : AbstractValidator<DiagnosisSubTypeViewModel>
    {
        public DiagnosisSubTypeViewModelValidator()
        {
            RuleFor(diagnosisSubType => diagnosisSubType.Name).NotEmpty()
                .Length(1, 100).WithMessage("Diagnosis Sub Type name must be between 1 - 100 characters");
        }
    }
}