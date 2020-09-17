using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Areas.Identity.Data
{
    public class NetCoreIdentityUserRole : IdentityUserRole<string>
    {
        public virtual NetCoreIdentityUser User { get; set; }
        public virtual NetCoreIdentityRole Role { get; set; }

       
    }
}
