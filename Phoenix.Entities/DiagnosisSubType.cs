using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class DiagnosisSubType : IEntityBase
    {
//        public DiagnosisSubType() { Diagnoses = new List<Diagnosis>(); } 
        public int ID { get; set; }
        public string Name { get; set; }
        public int DiagnosisId { get; set; }
        public virtual Diagnosis Diagnosis { get; set; }

//        public virtual ICollection<Diagnosis> Diagnoses { get; set; }

    }
}
