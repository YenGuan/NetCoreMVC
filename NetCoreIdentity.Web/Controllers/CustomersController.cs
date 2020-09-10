using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreIdentity.Model;
using NetCoreIdentity.Web.Paginations;

namespace NetCoreIdentity.Web
{
    public class CustomersController : BaseController
    {
        private readonly EFCoreLabContext _context;
        private readonly int pageSize;
        private readonly UnitOfWork<EFCoreLabContext> _unitOfWork;

      
#if UseDbContext
        public CustomersController(EFCoreLabContext context)
        {
            _context = context;
            pageSize = base._defaultPageSize;
        }  
#else
        public CustomersController(EFCoreLabContext context, IUnitOfWork unit)
        {
            _context = context;
            _unitOfWork = (UnitOfWork<EFCoreLabContext>)unit;
            pageSize = base._defaultPageSize;
        }
#endif

        // GET: Customers
        public async Task<IActionResult> Index(string keyword, string sortName, string sortOrder, int? pageNum)
        {

            ViewBag.SortName = sortName;
            ViewBag.SortOrder = !string.IsNullOrEmpty(sortOrder) ? sortOrder : "Asc";
            ViewBag.Keyword = keyword;
            pageNum ??= 1;
#if UseDbContext
            var customers = _context.Customer.Select(m => m);
#else
            var repo = _unitOfWork.GetRepository<Customer>();
            var customers = repo.All();
#endif            
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
#if UseDbContext
            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
#else
            var repo = _unitOfWork.GetRepository<Customer>();
            var customer = repo.FirstOrDefaultAsync(m => m.CustomerId == id);
#endif

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
#if UseDbContext
                 _context.Add(customer);
                await _context.SaveChangesAsync(); 
#else
                await _unitOfWork.GetRepository<Customer>().CreateAsync(customer);
#endif

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
#if UseDbContext
            var customer = await _context.Customer.FindAsync(id);
#else
            var customer = await _unitOfWork.GetRepository<Customer>().FindAsync(id);
#endif

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
#if UseDbContext
                 _context.Update(customer);
                 await _context.SaveChangesAsync();
#else
                    await _unitOfWork.GetRepository<Customer>().UpdateAsync(customer);
#endif

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
#if UseDbContext
            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
#else
            var customer = await _unitOfWork.GetRepository<Customer>().FirstOrDefaultAsync(m => m.CustomerId == id);
#endif

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
#if UseDbContext
             var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
#else
            var repo = _unitOfWork.GetRepository<Customer>();
            var customer = await repo.FindAsync(id);
            await repo.DeleteAsync(customer);

#endif

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
