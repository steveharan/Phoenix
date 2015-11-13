﻿using FluentValidation;
using Phoenix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Web.Infrastructure.Validators
{
    public class EthnicityViewModelValidator : AbstractValidator<EthnicityViewModel>
    {
        public EthnicityViewModelValidator()
        {
            RuleFor(ethnicity => ethnicity.EthnicityName).NotEmpty()
                .Length(1, 100).WithMessage("Ethnicity must be between 1 - 100 characters");
        }
    }
}