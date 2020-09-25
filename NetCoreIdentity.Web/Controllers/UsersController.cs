using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreIdentity.Model;
using NetCoreIdentity.Web.Areas.Identity.Data;
using NetCoreIdentity.Web.Paginations;

namespace NetCoreIdentity.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController
    {
        private readonly int pageSize;
        private readonly UnitOfWork<EFCoreLabContext> _unitOfWork;
        public UsersController(IUnitOfWork<EFCoreLabContext> unit, UserManager<NetCoreIdentityUser> userManager, RoleManager<NetCoreIdentityRole> roleManager)
           : base(userManager, roleManager)
        {
            _unitOfWork = (UnitOfWork<EFCoreLabContext>)unit;
            pageSize = base._defaultPageSize;
        }
        public async Task<IActionResult> Index(string keyword, string sortName, string sortOrder, int? pageNum)
        {

            ViewBag.SortName = sortName;
            ViewBag.SortOrder = !string.IsNullOrEmpty(sortOrder) ? sortOrder : "Asc";
            ViewBag.Keyword = keyword;
            pageNum ??= 1;
            var users = UserManager.Users;
            if (!string.IsNullOrEmpty(keyword))
            {
                users = users.Where(m =>
                m.UserName != null && m.UserName.Contains(keyword));
            }
            switch ($"{sortName}_{sortOrder}")
            {
                case "UserName_Asc":
                    users = users.OrderBy(m => m.UserName);
                    break;
                case "UserName_Desc":
                    users = users.OrderByDescending(m => m.UserName);
                    break;
                default:
                    break;
            }
            return View(await users.ToPaginatedListAsync(pageNum.Value, pageSize));
        }
    }
}
