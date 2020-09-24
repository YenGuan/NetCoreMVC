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
using NetCoreIdentity.Web.Filters;
using NetCoreIdentity.Web.Paginations;

namespace NetCoreIdentity.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppFunctionsController : BaseController
    {
        private readonly int pageSize;
        private readonly UnitOfWork<NetCoreIdentityContext> _unitOfWork;

        public AppFunctionsController(IUnitOfWork<NetCoreIdentityContext> unit, UserManager<NetCoreIdentityUser> userManager, RoleManager<NetCoreIdentityRole> roleManager)
           : base(userManager, roleManager)
        {
            _unitOfWork = (UnitOfWork<NetCoreIdentityContext>)unit;
            pageSize = base._defaultPageSize;
        }

        public async Task<IActionResult> Index(string keyword, string sortName, string sortOrder, int? pageNum)
        {

            ViewBag.SortName = sortName;
            ViewBag.SortOrder = !string.IsNullOrEmpty(sortOrder) ? sortOrder : "Asc";
            ViewBag.Keyword = keyword;
            pageNum ??= 1;

            var repo = _unitOfWork.GetRepository<AppFunction>();
            var AppFunctions = repo.All();

            if (!string.IsNullOrEmpty(keyword))
            {
                AppFunctions = AppFunctions.Where(m =>
                m.AppFunctionName != null && m.AppFunctionName.Contains(keyword)
                || m.ControllerName != null && m.ControllerName.Contains(keyword));
            }
            switch ($"{sortName}_{sortOrder}")
            {
                case "ControllerName_Asc":
                    AppFunctions = AppFunctions.OrderBy(m => m.ControllerName);
                    break;
                case "ControllerName_Desc":
                    AppFunctions = AppFunctions.OrderByDescending(m => m.ControllerName);
                    break;
                default:
                    break;
            }
            return View(await AppFunctions.ToPaginatedListAsync(pageNum.Value, pageSize));
        }

        // GET: AppFunctions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repo = _unitOfWork.GetRepository<AppFunction>();
            var AppFunction = await repo.FirstOrDefaultAsync(m => m.AppFunctionId == id);

            if (AppFunction == null)
            {
                return NotFound();
            }

            return View(AppFunction);
        }

        // GET: AppFunctions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppFunctions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppFunctionId,AppFunctionName,SchemaName,ControllerName")] AppFunction AppFunction)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.GetRepository<AppFunction>().CreateAsync(AppFunction);
                return RedirectToAction(nameof(Index));
            }
            return View(AppFunction);
        }

        // GET: AppFunctions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var AppFunction = await _unitOfWork.GetRepository<AppFunction>().FindAsync(id);

            if (AppFunction == null)
            {
                return NotFound();
            }
            return View(AppFunction);
        }

        // POST: AppFunctions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppFunctionId,AppFunctionName,SchemaName,ControllerName")] AppFunction AppFunction)
        {
            if (id != AppFunction.AppFunctionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.GetRepository<AppFunction>().UpdateAsync(AppFunction);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppFunctionExists(AppFunction.AppFunctionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(AppFunction);
        }

        // GET: AppFunctions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var AppFunction = await _unitOfWork.GetRepository<AppFunction>().FirstOrDefaultAsync(m => m.AppFunctionId == id);
            if (AppFunction == null)
            {
                return NotFound();
            }

            return View(AppFunction);
        }

        // POST: AppFunctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repo = _unitOfWork.GetRepository<AppFunction>();
            var AppFunction = await repo.FindAsync(id);
            await repo.DeleteAsync(AppFunction);

            return RedirectToAction(nameof(Index));
        }

        private bool AppFunctionExists(int id)
        {
            return _unitOfWork.GetRepository<AppFunction>().All().Any(e => e.AppFunctionId == id);
        }

        public async Task<IActionResult> MappingRoles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var AppFunction = await _unitOfWork.GetRepository<AppFunction>().FirstOrDefaultAsync(m => m.AppFunctionId == id);
            if (AppFunction == null)
            {
                return NotFound();
            }

            return PartialView("_MappingRoles");
        }
    }
}
