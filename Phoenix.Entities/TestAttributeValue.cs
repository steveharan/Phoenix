using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class TestAttributeValue : IEntityBase
    {
        public int ID { get; set; }
        public int TestAttributeValueID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
        public TestAttributeValue()
        {
            TestAttributes = new HashSet<TestAttribute>();
        }
        public virtual ICollection<TestAttribute> TestAttributes { get; set; }
    }
}
