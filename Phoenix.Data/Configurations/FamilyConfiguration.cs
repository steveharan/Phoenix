using Phoenix.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Data.Configurations
{
    public class FamilyConfiguration : EntityBaseConfiguration<Family>
    {
        public FamilyConfiguration()
        {
            Property(u => u.FamilyName).IsRequired().HasMaxLength(30);
            Property(u => u.FirstRegisteredDate).IsRequired();
            Property(u => u.Notes).IsRequired().HasMaxLength(100);
            Property(u => u.EthnicityID).IsRequired();
        }
    }
}
