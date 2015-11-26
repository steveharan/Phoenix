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
        public static void UpdateFamily(this Family family, FamilyViewModel familyVm)
        {
            family.FamilyName = familyVm.FamilyName;
            family.FirstRegisteredDate = familyVm.FirstRegisteredDate;
            family.Notes = familyVm.Notes;
            family.EthnicityID = familyVm.EthnicityID;
            if (familyVm.DiagnosisID == 0)
            {
                familyVm.DiagnosisID = null;
            }
            if (familyVm.DiagnosisSubTypeID == 0)
            {
                familyVm.DiagnosisSubTypeID = null;
            }
            family.DiagnosisID = familyVm.DiagnosisID;
            family.DiagnosisSubTypeId = familyVm.DiagnosisSubTypeID;
            family.Deleted = familyVm.Deleted;
            Random rnd = new Random();
            int number = rnd.Next(1000, 1000000);
            if (family.FamilyIdentifier == null)
            {
                family.FamilyIdentifier = "F" + number.ToString();
            }
        }

        public static void UpdatePerson(this Person person, PersonViewModel personVm)
        {
            person.NhsNumber = personVm.NhsNumber;
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
                person.DateDeceased = DateTime.MinValue;
            }
            else
            {
                person.DateDeceased = personVm.DateDeceased;
            }
            person.FirstRegisteredDate = personVm.FirstRegisteredDate;
            person.Notes = personVm.Notes;
            if (personVm.DiagnosisID == 0)
            {
                personVm.DiagnosisID = null;
            }
            if (personVm.DiagnosisSubTypeID == 0)
            {
                personVm.DiagnosisSubTypeID = null;
            }
            person.DiagnosisId = personVm.DiagnosisID;
            person.DiagnosisSubTypeId = personVm.DiagnosisSubTypeID;
            person.EthnicityId = personVm.EthnicityID;
            person.FamilyId = personVm.FamilyID;
            person.Deleted = personVm.Deleted;
            person.Gender = personVm.Gender;
        }

        public static void UpdatePersonRelationship(this PersonRelationship personRelationship, PersonRelationshipViewModel personRelationshipVM)
        {
            personRelationship.RelationshipFromPersonId = personRelationshipVM.PersonId;
            personRelationship.RelationWithPersonId = personRelationshipVM.RelationWithPersonId;
            personRelationship.RelationshipTypeId = personRelationshipVM.RelationshipTypeId;
        }
    }
}