using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class Test : IEntityBase
    {
        public int ID { get; set; }
        public virtual TestType TestType { get; set; }
        public int TestTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public Test()
        {
            TestAttributes = new List<TestAttribute>();
            PatientTests = new List<PatientTest>();
        }
        public virtual ICollection<TestAttribute> TestAttributes { get; set; }
        public virtual ICollection<PatientTest> PatientTests { get; set; }

    }
}
