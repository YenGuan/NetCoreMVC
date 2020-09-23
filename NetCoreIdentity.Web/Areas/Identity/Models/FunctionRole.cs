using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Areas.Identity.Data
{
    public class FunctionRole
    {
        public int AppFunctionId { get; set; }
        public string RoleId { get; set; }
        public NetCoreIdentityRole Role { get; set; }
        public AppFunction Function { get; set; }
    }
}
