using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartProfil.Data;
using SmartProfil.Models;

namespace SmartProfil.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class ProductsEditController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsEditController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.AddedByUser).Include(p => p.Category).Include(p => p.Manufacturer).Include(p => p.ProductMaterialType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/ProductsEdit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.AddedByUser)
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductMaterialType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["AddedByUserId"] = new SelectList(_context.Users, "Id", "Id", product.AddedByUserId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Country", product.ManufacturerId);
            ViewData["ProductMaterialTypeId"] = new SelectList(_context.ProductMaterialTypes, "Id", "Name", product.ProductMaterialTypeId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,CategoryId,ProductMaterialTypeId,ManufacturerId,Description,Specifications,UnitPrice,UnitsInStock,Weight,Length,Width,AddedByUserId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["AddedByUserId"] = new SelectList(_context.Users, "Id", "Id", product.AddedByUserId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Country", product.ManufacturerId);
            ViewData["ProductMaterialTypeId"] = new SelectList(_context.ProductMaterialTypes, "Id", "Name", product.ProductMaterialTypeId);
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.AddedByUser)
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductMaterialType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
