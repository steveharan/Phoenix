using FluentValidation;
using Phoenix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Web.Infrastructure.Validators
{
    public class RelationshipTypeViewModelValidator : AbstractValidator<RelationshipTypeViewModel>
    {
        public RelationshipTypeViewModelValidator()
        {
            RuleFor(relationshipType => relationshipType.RelationshipTypeName).NotEmpty()
                .Length(1, 100).WithMessage("Relationship type name must be between 1 - 100 characters");
        }
    }
}