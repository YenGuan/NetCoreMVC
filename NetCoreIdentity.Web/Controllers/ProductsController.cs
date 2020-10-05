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
    public class ProductsController : BaseController
    {
        private readonly int pageSize;
        private readonly UnitOfWork<EFCoreLabContext> _unitOfWork;

        public ProductsController(IUnitOfWork<EFCoreLabContext> unit, UserManager<NetCoreIdentityUser> userManager, RoleManager<NetCoreIdentityRole> roleManager)
           : base(userManager, roleManager)
        {
            _unitOfWork = (UnitOfWork<EFCoreLabContext>)unit;
            pageSize = base._defaultPageSize;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var eFCoreLabContext = _unitOfWork.GetRepository<Product>().All().Include(p => p.ProductCategory).Include(p => p.ProductModel);
            return View(await eFCoreLabContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _unitOfWork.GetRepository<Product>().All()
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductCategoryId"] = new SelectList(_unitOfWork.GetRepository<ProductCategory>().All(), "ProductCategoryId", "Name");
            ViewData["ProductModelId"] = new SelectList(_unitOfWork.GetRepository<ProductModel>().All(), "ProductModelId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,Rowguid,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.GetRepository<Product>().CreateAsync(product);               
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_unitOfWork.GetRepository<ProductCategory>().All(), "ProductCategoryId", "Name");
            ViewData["ProductModelId"] = new SelectList(_unitOfWork.GetRepository<ProductModel>().All(), "ProductModelId", "Name");
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _unitOfWork.GetRepository<Product>().FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_unitOfWork.GetRepository<ProductCategory>().All(), "ProductCategoryId", "Name");
            ViewData["ProductModelId"] = new SelectList(_unitOfWork.GetRepository<ProductModel>().All(), "ProductModelId", "Name");
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,Rowguid,ModifiedDate")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _unitOfWork.GetRepository<Product>().UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["ProductCategoryId"] = new SelectList(_unitOfWork.GetRepository<ProductCategory>().All(), "ProductCategoryId", "Name");
            ViewData["ProductModelId"] = new SelectList(_unitOfWork.GetRepository<ProductModel>().All(), "ProductModelId", "Name");
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _unitOfWork.GetRepository<Product>().All()
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _unitOfWork.GetRepository<Product>().FindAsync(id);
            await _unitOfWork.GetRepository<Product>().DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _unitOfWork.GetRepository<Product>().All().Any(e => e.ProductId == id);
        }
    }
}
