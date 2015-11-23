using Phoenix.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Data.Configurations
{
    public class PersonConfiguration : EntityBaseConfiguration<Person>
    {
        public PersonConfiguration()
        {
            Property(u => u.FirstName).IsRequired().HasMaxLength(30);
            Property(u => u.SurName).IsRequired().HasMaxLength(30);
            Property(u => u.DateOfBirth).IsRequired();
            Property(u => u.HeightCM).IsRequired();
            Property(u => u.WeightKG).IsRequired();
            Property(u => u.Deceased).IsRequired();
            Property(u => u.Gender).IsRequired();
            Property(u => u.DateDeceased).IsRequired();
            Property(u => u.FirstRegisteredDate).IsRequired();
            Property(u => u.Notes).IsRequired();
            Property(u => u.Adopted).IsRequired();
            Property(u => u.Twin).IsRequired();
            Property(u => u.EthnicityId).IsRequired();
            Property(u => u.FamilyId).IsRequired();
            Property(u => u.DiagnosisId).IsOptional();
            Property(u => u.DiagnosisSubTypeId).IsOptional();
            Property(u => u.Deleted).IsOptional();
        }
    }
}
