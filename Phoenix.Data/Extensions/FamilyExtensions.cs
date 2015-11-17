using Phoenix.Data.Repositories;
using Phoenix.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Data.Extensions
{
    public static class FamilyExtensions
    {
        public static bool FamilyExists(this IEntityBaseRepository<Family> familyRepository, string familyName)
        {
            bool _familyExists = false;

            _familyExists = familyRepository.FindBy(c => c.Deleted == false)
                .Any(c => c.FamilyName.ToLower() == familyName);

            return _familyExists;
        }
    }
}
