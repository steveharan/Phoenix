using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class Ethnicity : IEntityBase
    {
        public Ethnicity()
        {
            Families = new List<Family>();
        }
        public int ID { get; set; }
        public string EthnicityName { get; set; }
        public virtual ICollection<Family> Families { get; set; }
    }
}
