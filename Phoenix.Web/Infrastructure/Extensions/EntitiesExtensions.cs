using Phoenix.Entities;
using Phoenix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Web.Infrastructure.Extensions
{
    public static class EntitiesExtensions
    {
        public static void UpdateMovie(this Movie movie, MovieViewModel movieVm)
        {
            movie.Title = movieVm.Title;
            movie.Description = movieVm.Description;
            movie.GenreId = movieVm.GenreId;
            movie.Director = movieVm.Director;
            movie.Writer = movieVm.Writer;
            movie.Producer = movieVm.Producer;
            movie.Rating = movieVm.Rating;
            movie.TrailerURI = movieVm.TrailerURI;
            movie.ReleaseDate = movieVm.ReleaseDate;
        }

        public static void UpdateCustomer(this Customer customer, CustomerViewModel customerVm)
        {
            customer.FirstName = customerVm.FirstName;
            customer.LastName = customerVm.LastName;
            customer.IdentityCard = customerVm.IdentityCard;
            customer.Mobile = customerVm.Mobile;
            customer.DateOfBirth = customerVm.DateOfBirth;
            customer.Email = customerVm.Email;
            customer.UniqueKey = (customerVm.UniqueKey == null || customerVm.UniqueKey == Guid.Empty)
                ? Guid.NewGuid() : customerVm.UniqueKey;
            customer.RegistrationDate = (customer.RegistrationDate == DateTime.MinValue ? DateTime.Now : customerVm.RegistrationDate);
        }

        public static void UpdateFamily(this Family family, FamilyViewModel familyVm)
        {
            family.FamilyName = familyVm.FamilyName;
            family.FirstRegisteredDate = familyVm.FirstRegisteredDate;
            family.Notes = familyVm.Notes;
            family.EthnicityID = familyVm.EthnicityID;
            family.DiagnosisID = familyVm.DiagnosisID;
            family.DiagnosisSubTypeId = familyVm.DiagnosisSubTypeID;
            family.Deleted = familyVm.Deleted;
        }

        public static void UpdatePerson(this Person person, PersonViewModel personVm)
        {
            person.FirstName = personVm.FirstName;
            person.SurName = personVm.SurName;
            person.DateOfBirth = personVm.DateOfBirth;
            person.Adopted = personVm.Adopted;
            person.Twin = personVm.Twin;
            person.WeightKG = personVm.WeightKG;
            person.HeightCM = personVm.HeightCM;
            person.Deceased = personVm.Deceased;
            if (personVm.DateDeceased == null)
            {
                person.DateDeceased = DateTime.Now;
            }
            else
            {
                person.DateDeceased = personVm.DateDeceased;
            }
            person.FirstRegisteredDate = personVm.FirstRegisteredDate;
            person.Notes = personVm.Notes;
            person.DiagnosisId = personVm.DiagnosisID;
            person.DiagnosisSubTypeId = personVm.DiagnosisSubTypeID;
            person.EthnicityId = personVm.EthnicityID;
            person.FamilyId = personVm.FamilyID;
            person.Deleted = personVm.Deleted;
        }

        public static void UpdatePersonRelationship(this PersonRelationship personRelationship, PersonRelationshipViewModel personRelationshipVM)
        {
            personRelationship.PersonId = personRelationshipVM.PersonId;
            personRelationship.RelationWithPersonId = personRelationshipVM.RelationWithPersonId;
            personRelationship.RelationshipType = personRelationshipVM.RelationshipType;
        }
    }
}