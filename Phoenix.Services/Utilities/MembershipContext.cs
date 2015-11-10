using Phoenix.Entities;
using System.Security.Principal;

namespace Phoenix.Services
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public User user { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }
    }
}