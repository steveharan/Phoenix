using FluentValidation;
using Phoenix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Web.Infrastructure.Validators
{
    public class PersonRelationshipViewModelValidator : AbstractValidator<PersonRelationshipViewModel>
    {
        public PersonRelationshipViewModelValidator()
        {
            RuleFor(r => r.PersonId).NotEmpty()
                .WithMessage("Please select person.");
            RuleFor(r => r.RelationWithPersonId).NotEmpty()
                .WithMessage("Please the person having relationship with.");
            RuleFor(r => r.RelationshipType).NotEmpty()
                .WithMessage("Please select relationship type.");
        }
    }
}