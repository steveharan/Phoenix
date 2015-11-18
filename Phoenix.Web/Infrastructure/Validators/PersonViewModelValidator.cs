using FluentValidation;
using Phoenix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Web.Infrastructure.Validators
{
    public class PersonViewModelValidator : AbstractValidator<PersonViewModel>
    {
        public PersonViewModelValidator()
        {
            RuleFor(r => r.FirstName).NotEmpty()
                .WithMessage("Please enter the first name.");
            RuleFor(r => r.SurName).NotEmpty()
                .WithMessage("Please enter the surname.");
            RuleFor(r => r.DateOfBirth).NotEmpty()
                .WithMessage("Please enter the date of birth.");
            RuleFor(r => r.EthnicityID).NotEmpty()
                .WithMessage("Please select the ethnicity.");
        }
    }
}