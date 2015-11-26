using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class Family : IEntityBase
    {
        public Family() { Persons = new List<Person>(); }
        public int ID { get; set; }
        public DateTime FirstRegisteredDate { get; set; }
        public string Notes { get; set; }
        public string FamilyName { get; set; }
        public virtual Ethnicity Ethnicity { get; set; }
        public int? EthnicityID { get; set; }
        public int? DiagnosisID { get; set; }
        public virtual Diagnosis Diagnosis { get; set; }
        public int? DiagnosisSubTypeId { get; set; }
        public virtual DiagnosisSubType DiagnosisSubType { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public string FamilyIdentifier { get; set; }
    }
}
