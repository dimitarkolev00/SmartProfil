﻿using System.Linq;
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
        private readonly ApplicationDbContext db;

        public ProductsEditController(ApplicationDbContext context)
        {
            this.db = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.db.Products.Where(x => x.IsDeleted == false)
                .Include(p => p.AddedByUser)
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductMaterialType);

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = await this.db.Products
                .Include(p => p.AddedByUser)
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductMaterialType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return this.NotFound();
            }

            return this.View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = await this.db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id", product.AddedByUserId);
            ViewData["CategoryId"] = new SelectList(this.db.Categories, "Id", "Name", product.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(this.db.Manufacturers, "Id", "Name", product.ManufacturerId);
            ViewData["ProductMaterialTypeId"] = new SelectList(this.db.ProductMaterialTypes, "Id", "Name", product.ProductMaterialTypeId);
            return this.View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,CategoryId,ProductMaterialTypeId,ManufacturerId,Description,Specifications,UnitPrice,UnitsInStock,Weight,Length,Width,AddedByUserId")] Product product)
        {
            if (id != product.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.db.Update(product);
                    await this.db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return this.RedirectToAction(nameof(Index));
            }
            ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id", product.AddedByUserId);
            ViewData["CategoryId"] = new SelectList(this.db.Categories, "Id", "Name", product.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(this.db.Manufacturers, "Id", "Name", product.ManufacturerId);
            ViewData["ProductMaterialTypeId"] = new SelectList(this.db.ProductMaterialTypes, "Id", "Name", product.ProductMaterialTypeId);
            return this.View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = await this.db.Products
                .Include(p => p.AddedByUser)
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductMaterialType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return this.NotFound();
            }

            return this.View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await this.db.Products.FindAsync(id);
            product.IsDeleted = true;

            this.db.Products.Update(product);
            await this.db.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return this.db.Products.Any(e => e.Id == id);
        }
    }
}
