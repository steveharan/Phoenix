using Phoenix.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Data.Configurations
{
    public class DiagnosisConfiguration : EntityBaseConfiguration<Diagnosis>
    {
        public DiagnosisConfiguration()
        {
            Property(u => u.Name).IsRequired().HasMaxLength(50);
        }
    }
}
