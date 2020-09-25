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
    public class RolesController : BaseController
    {
        private readonly int pageSize;
        private readonly UnitOfWork<EFCoreLabContext> _unitOfWork;
        public RolesController(IUnitOfWork<EFCoreLabContext> unit, UserManager<NetCoreIdentityUser> userManager, RoleManager<NetCoreIdentityRole> roleManager)
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
            var roles = RoleManager.Roles;
            if (!string.IsNullOrEmpty(keyword))
            {
                roles = roles.Where(m =>
                m.Name != null && m.Name.Contains(keyword)
                || m.NormalizedName != null && m.NormalizedName.Contains(keyword));
            }
            switch ($"{sortName}_{sortOrder}")
            {
                case "Name_Asc":
                    roles = roles.OrderBy(m => m.Name);
                    break;
                case "Name_Desc":
                    roles = roles.OrderByDescending(m => m.Name);
                    break;
                default:
                    break;
            }
            return View(await roles.ToPaginatedListAsync(pageNum.Value, pageSize));
        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,NormalizedName")] NetCoreIdentityRole role)
        {
            if (string.IsNullOrEmpty(role.Name))
            {
                ModelState.AddModelError("Name", "Name is required");
            }
            //if (string.IsNullOrEmpty(role.NormalizedName))
            //{
            //    ModelState.AddModelError("NormalizedName", "NormalizedName is required");
            //}
            if (ModelState.IsValid)
            {
                role.NormalizedName = role.Name.ToUpper();
                await RoleManager.CreateAsync(role);
                return Json(Url.Action(nameof(Index)));
            }
            return PartialView("_Create", role);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return PartialView("_Edit", role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,NormalizedName")] NetCoreIdentityRole role)
        {
            var roleData = await RoleManager.FindByIdAsync(role.Id);
            if (roleData == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(role.Name))
            {
                ModelState.AddModelError("Name", "Name is required");
            }
            
            if (ModelState.IsValid)
            {
                roleData.Name = role.Name;
                roleData.NormalizedName = role.Name.ToUpper();
                await RoleManager.UpdateAsync(role);
                return Json(Url.Action(nameof(Index)));
            }
            return PartialView("_Edit", role);
        }
    }
}
