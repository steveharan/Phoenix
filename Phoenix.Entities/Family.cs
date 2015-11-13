using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class Family : IEntityBase
    {
        public int ID { get; set; }
        public DateTime FirstRegisteredDate { get; set; }
        public string Notes { get; set; }
        public string FamilyName { get; set; }
        public int PrimaryRace { get; set; }
        public int PrimaryEthnicity { get; set; }
    }
}
