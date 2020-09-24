using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Models
{
    public class FuncRoleMappingViewModel
    {
        public bool Selected { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public int? AppFunctionId { get; set; }
        public string AppFunctionName { get; set; }
    }
}
