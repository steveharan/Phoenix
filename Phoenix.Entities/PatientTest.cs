using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class PatientTest : IEntityBase
    {
        public int ID { get; set; }
        public DateTime TestDateTime { get; set; }
        public string Notes { get; set; }
        public int TestID { get; set; }
        public virtual Person Person { get; set; }
        public virtual Test Test { get; set; }
    }
}