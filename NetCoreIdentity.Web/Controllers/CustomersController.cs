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

namespace NetCoreIdentity.Web
{
    //ref https://stackoverflow.com/questions/29915192/dependency-injection-in-attributes/62469156#62469156?newreg=8868f27dce4946c79826fce2fca92cb3
    [TypeFilter(typeof(FunctionAuthorizeAttribute))]
    public class CustomersController : BaseController
    {
        private readonly int pageSize;
        private readonly UnitOfWork<EFCoreLabContext> _unitOfWork;

        public CustomersController(IUnitOfWork<EFCoreLabContext> unit, UserManager<NetCoreIdentityUser> userManager, RoleManager<NetCoreIdentityRole> roleManager)
           : base(userManager, roleManager)
        {
            _unitOfWork = (UnitOfWork<EFCoreLabContext>)unit;
            pageSize = base._defaultPageSize;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string keyword, string sortName, string sortOrder, int? pageNum)
        {

            ViewBag.SortName = sortName;
            ViewBag.SortOrder = !string.IsNullOrEmpty(sortOrder) ? sortOrder : "Asc";
            ViewBag.Keyword = keyword;
            pageNum ??= 1;

            var repo = _unitOfWork.GetRepository<Customer>();
            var customers = repo.All();

            if (!string.IsNullOrEmpty(keyword))
            {
                customers = customers.Where(m =>
                m.FirstName != null && m.FirstName.Contains(keyword)
                || m.LastName != null && m.LastName.Contains(keyword));
            }
            switch ($"{sortName}_{sortOrder}")
            {
                case "FirstName_Asc":
                    customers = customers.OrderBy(m => m.FirstName);
                    break;
                case "FirstName_Desc":
                    customers = customers.OrderByDescending(m => m.FirstName);
                    break;
                default:
                    break;
            }
            return View(await customers.ToPaginatedListAsync(pageNum.Value, pageSize));
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repo = _unitOfWork.GetRepository<Customer>();
            var customer = await repo.FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,CompanyName,SalesPerson,EmailAddress,Phone,PasswordHash,PasswordSalt,Rowguid,ModifiedDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.GetRepository<Customer>().CreateAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _unitOfWork.GetRepository<Customer>().FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,CompanyName,SalesPerson,EmailAddress,Phone,PasswordHash,PasswordSalt,Rowguid,ModifiedDate")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.GetRepository<Customer>().UpdateAsync(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _unitOfWork.GetRepository<Customer>().FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repo = _unitOfWork.GetRepository<Customer>();
            var customer = await repo.FindAsync(id);
            await repo.DeleteAsync(customer);

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _unitOfWork.GetRepository<Customer>().All().Any(e => e.CustomerId == id);
        }
    }
}
