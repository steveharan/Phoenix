﻿using Phoenix.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Data.Configurations
{
    public class PersonRelationshipConfiguration : EntityBaseConfiguration<PersonRelationship>
    {
        public PersonRelationshipConfiguration()
        {
            Property(u => u.RelationshipFromPersonId).IsRequired();
            Property(u => u.RelationWithPersonId).IsRequired();
            Property(u => u.RelationshipTypeId).IsRequired();
        }
    }
}
