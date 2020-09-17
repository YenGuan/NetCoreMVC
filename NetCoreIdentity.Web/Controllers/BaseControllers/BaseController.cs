using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreIdentity.Web.Areas.Identity.Data;

namespace NetCoreIdentity.Web
{
    public class BaseController : Controller
    {
        public BaseController(UserManager<NetCoreIdentityUser> userManager, RoleManager<NetCoreIdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        #region Identity
        public UserManager<NetCoreIdentityUser> UserManager { get; private set; }
        public RoleManager<NetCoreIdentityRole> RoleManager { get; private set; }
        #endregion

        protected readonly int _defaultPageSize = 10;
        private int _PageSize;
        public int PageSize
        {
            get { return this._PageSize > 0 ? this._PageSize : _defaultPageSize; }
            set { this._PageSize = value; }
        }

       
      
    }
}
