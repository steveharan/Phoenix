using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class RelationshipType : IEntityBase
    {
        public RelationshipType() { PersonRelationships = new List<PersonRelationship>(); }
        public int ID { get; set; }
        public string RelationshipTypeName { get; set; }
        public virtual ICollection<PersonRelationship> PersonRelationships { get; set; }
    }
}
