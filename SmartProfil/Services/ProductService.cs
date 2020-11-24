using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Models.Enum;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext db;

        public ProductService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Add(ProductInputModel productModel)
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
                ImageSource = productModel.ImageSource
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();

        }
    }
}
