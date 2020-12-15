using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SmartProfil.AutoMapper;
using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services
{
    public class ProductService : IProductService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif", "jpeg", "tif" };
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment environment;

        public ProductService(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            this.db = db;
            this.environment = environment;
        }
        public async Task CreateAsync(ProductInputModel productModel, string userId)
        {
            var product = new Product
            {
                Name = productModel.Name,
                CategoryId = productModel.CategoryId,
                Description = productModel.Description,
                ManufacturerId = productModel.ManufacturerId,
                Specifications = productModel.Specifications,
                Model = productModel.Model,
                UnitPrice = productModel.UnitPrice,
                ProductMaterialTypeId = productModel.ProductMaterialTypeId,
                Length = productModel.Length,
                Weight = productModel.Weight,
                Width = productModel.Width,
                UnitsInStock = productModel.UnitsInStock,
                AddedByUserId = userId,
                IsDeleted = false
            };


            foreach (var image in productModel.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                var wwwrootPath = this.environment.WebRootPath;

                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension} !");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension
                };

                product.Images.Add(dbImage);

                var physicalPath = $"{wwwrootPath}/images/products/{dbImage.Id}.{extension}";
                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.db.Products.AddAsync(product);
            await this.db.SaveChangesAsync();

        }

        public IEnumerable<T> GetAll<T>(int page, int productsPerPage )
        {
            var products = this.db.Products
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage)
                .To<T>()
                .ToList();

            return products;
        }

        public IEnumerable<T> GetAllProfiles<T>(int page, int productsPerPage )
        {
            var products = this.db.Products
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Category.Name == "Profile")
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage)
                .To<T>()
                .ToList();

            return products;
        }

        public IEnumerable<T> GetAllAccessories<T>(int page, int productsPerPage)
        {
            var products = this.db.Products
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Category.Name == "Accessories")
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage)
                .To<T>()
                .ToList();

            return products;
        }

        public IEnumerable<T> GetAllWindowSills<T>(int page, int productsPerPage)
        {
            var products = this.db.Products
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Category.Name == "Window Sill")
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage)
                .To<T>()
                .ToList();

            return products;
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.db.Products.Where(x => x.IsDeleted == false)
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .To<T>()
                .ToList();
        }

       

        public T GetById<T>(int id)
        {
            var product = this.db.Products.Where(x => x.IsDeleted == false)
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return product;
        }

        public Product GetProductById(int productId)
        {

            return this.db.Products.FirstOrDefault(x => x.Id == productId);

            //if (withNavigationalProperties)
            //{
            //    return context.Products
            //        .Include(x => x.ProductComments)
            //        .ThenInclude(pc => pc.User)
            //        .Include(x => x.ProductRatings)
            //        .ThenInclude(x => x.User)
            //        .Include(x => x.AdditionalImages)
            //        .FirstOrDefault(x => x.Id == productId);
            //}
            //else
            //{
            //    return context.Products.FirstOrDefault(x => x.Id == productId);
            //}
        }

        public int GetCount()
        {
            return this.db.Products.Count(x => x.IsDeleted == false);
        }

        public int GetProfilesCount()
        {
            return this.db.Products.Count
                (x => x.IsDeleted == false && x.Category.Name=="Profile");
        }

        public int GetAccessoriesCount()
        {
            return this.db.Products.Count
                (x => x.IsDeleted == false && x.Category.Name == "Accessories");
        }

        public int GetWindowSillsCount()
        {
            return this.db.Products.Count
                (x => x.IsDeleted == false && x.Category.Name == "Window Sill");
        }
    }
}
