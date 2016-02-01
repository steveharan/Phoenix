using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class TestAttribute : IEntityBase
    {
        public int ID { get; set; }
        public virtual Test Test { get; set; }
        public int TestID { get; set; }
        public string FieldType { get; set; }
        public decimal FieldSize { get; set; }
        public string FieldLabel { get; set; }
        public bool Mandatory { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
    }
}
