using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Areas.Identity.Data
{
    public class AppFunction
    {
        public AppFunction()
        {
            FunctionRoles = new HashSet<FunctionRole>();
        }
        public int AppFunctionId { get; set; }
        public string AppFunctionName { get; set; }
        public string SchemaName { get; set; }
        public string ControllerName { get; set; }
     
        public virtual ICollection<FunctionRole> FunctionRoles { get; set; }
    }
}
