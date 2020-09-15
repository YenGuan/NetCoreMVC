using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NetCoreIdentity.Web.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the NetCoreIdentityUser class
    public class NetCoreIdentityUser : IdentityUser
    {
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<NetCoreIdentityUserRole> UserRoles { get; set; }
    }
}
