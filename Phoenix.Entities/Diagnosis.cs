using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class Diagnosis : IEntityBase
    {
        public Diagnosis() { Persons = new List<Person>(); }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
    }
}
