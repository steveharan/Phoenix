using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class Person : IEntityBase
    {
        public Person() { PersonRelationships = new List<PersonRelationship>(); }

        public int ID { get; set; }
        public string NhsNumber { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Twin { get; set; }
        public bool Adopted { get; set; }
        public decimal HeightCM { get; set; }
        public decimal WeightKG { get; set; }
        public bool Deceased { get; set; }
        public DateTime? DateDeceased { get; set; }
        public DateTime FirstRegisteredDate { get; set; }
        public string Gender { get; set; }
        public string Notes { get; set; }
        public int FamilyId { get; set; }
        public virtual Family Family { get; set; }
        public int? DiagnosisId { get; set; }
        public virtual Diagnosis Diagnosis { get; set; }
        public int? DiagnosisSubTypeId { get; set; }
        public virtual DiagnosisSubType DiagnosisSubType { get; set; }
        public int? EthnicityId { get; set; }
        public virtual Ethnicity Ethnicity { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<PersonRelationship> PersonRelationships { get; set; }

    }
}
