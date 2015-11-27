using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class PersonRelationship : IEntityBase
    {
        public int ID { get; set; }
        public int RelationshipFromPersonId { get; set; }
        [ForeignKey("RelationshipFromPersonId")]
        [InverseProperty("PersonRelationships")]
        public virtual Person relationFromPerson { get; set; }
        public int RelationWithPersonId { get; set; }
        [ForeignKey("RelationWithPersonId")]
        public virtual Person relationWithPerson { get; set; }
        public int RelationshipTypeId { get; set; }
        public virtual RelationshipType RelationshipType { get; set; }
    }
}
