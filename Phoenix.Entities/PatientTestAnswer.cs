using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class PatientTestAnswer : IEntityBase
    {
        public int ID { get; set; }
        public string ValueString { get; set; }
        public int? ValueInt { get; set; }
        public decimal? ValueDec { get; set; }
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }
        public int TestAttributeID { get; set; }
        public virtual TestAttribute TestAttribute { get; set; }
    }
}
