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
    [TypeFilter(typeof(FunctionAuthorizeAttribute))]
    public class AddressesController :  BaseController
    {
        private readonly int pageSize;
        private readonly UnitOfWork<EFCoreLabContext> _unitOfWork;

        public AddressesController(IUnitOfWork<EFCoreLabContext> unit, UserManager<NetCoreIdentityUser> userManager, RoleManager<NetCoreIdentityRole> roleManager)
         : base(userManager, roleManager)
        {
            _unitOfWork = (UnitOfWork<EFCoreLabContext>)unit;
            pageSize = base._defaultPageSize;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.GetRepository<Address>().FindAllAsync());
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _unitOfWork.GetRepository<Address>()
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressId,AddressLine1,AddressLine2,City,StateProvince,CountryRegion,PostalCode,Rowguid,ModifiedDate")] Address address)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.GetRepository<Address>().CreateAsync(address);             
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _unitOfWork.GetRepository<Address>().FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressId,AddressLine1,AddressLine2,City,StateProvince,CountryRegion,PostalCode,Rowguid,ModifiedDate")] Address address)
        {
            if (id != address.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _unitOfWork.GetRepository<Address>().UpdateAsync(address);                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressId))
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
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _unitOfWork.GetRepository<Address>()
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repo = _unitOfWork.GetRepository<Address>();
            var address = await repo.FindAsync(id);
            await repo.DeleteAsync(address);          
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _unitOfWork.GetRepository<Address>().All().Any(e => e.AddressId == id);
        }
    }
}
