using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class PersonRelationship : IEntityBase
    {
        public int ID { get; set; }
        public int PersonId { get; set; }
        public int RelationWithPersonId { get; set; }
        public virtual Person person { get; set; }
        public virtual Person relationWithPerson { get; set; }
        public int RelationshipTypeId { get; set; }
        public virtual RelationshipType RelationshipType { get; set; }
    }
}
