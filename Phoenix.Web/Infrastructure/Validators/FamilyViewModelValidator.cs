﻿using FluentValidation;
using Phoenix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Web.Infrastructure.Validators
{
    public class FamilyViewModelValidator : AbstractValidator<FamilyViewModel>
    {
        public FamilyViewModelValidator()
        {
            RuleFor(r => r.FamilyName).NotEmpty()
                .WithMessage("Please enter the family name.");
            RuleFor(r => r.FirstRegisteredDate).NotEmpty()
                .WithMessage("Please select first registered date.");

            RuleFor(r => r.Notes).NotEmpty()
                .WithMessage("Please add some notes about this family.");

            RuleFor(r => r.EthnicityID).NotEmpty()
                .WithMessage("Please select the primary ethnicity for this family.");
        }
    }
}