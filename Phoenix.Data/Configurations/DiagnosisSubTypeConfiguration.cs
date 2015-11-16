using Phoenix.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Data.Configurations
{
    public class DiagnosisSubTypeConfiguration : EntityBaseConfiguration<DiagnosisSubType>
    {
        public DiagnosisSubTypeConfiguration()
        {
            Property(u => u.Name).IsRequired().HasMaxLength(50);
            Property(u => u.DiagnosisId).IsRequired();
        }
    }
}
