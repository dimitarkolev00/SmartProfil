using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels;

namespace SmartProfil.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext db;

        public CartService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddToCartAsync(int productId, string userId, int quantity = 1)
        {
            if (this.IsProductInCart(productId, userId))
            {
                var productCart = GetProductFromCart(productId, userId);
                productCart.Quantity += quantity;
            }
            else
            {
                await this.db.ProductCarts.AddAsync(new ProductCart
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    IsDeleted = false
                });
            }

            await this.db.SaveChangesAsync();
        }

        public async Task RemoveProductByIdAsync(string userId, int productId)
        {
            this.db.ProductCarts.Remove(GetProductFromCart(productId, userId));
            await this.db.SaveChangesAsync();
        }

        public List<ProductCartViewModel> GetAllProductsForCartViewModel(string userId)
        {
            return this.db.ProductCarts
                .Where(x => x.UserId == userId)
                .Where(x => x.IsDeleted == false)
                .Select(x => new ProductCartViewModel
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Model = x.Product.Model,
                    ManufacturerName = x.Product.Manufacturer.Name,
                    Quantity = x.Quantity,
                    SinglePrice = x.Product.UnitPrice,
                    Image = "/images/products/" + x.Product.Images.FirstOrDefault().Id + "." + x.Product.Images.FirstOrDefault().Extension
                }).ToList();
        }

        public List<ProductCartViewModel> GetAllPreviousOrders(string userId)
        {
            return this.db.ProductCarts
                .Where(x => x.UserId == userId)
                .Where(x => x.IsDeleted == true)
                .Select(x => new ProductCartViewModel
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Model = x.Product.Model,
                    ManufacturerName = x.Product.Manufacturer.Name,
                    Quantity = x.Quantity,
                    SinglePrice = x.Product.UnitPrice,
                    Image = "/images/products/" + x.Product.Images.FirstOrDefault().Id + "." + x.Product.Images.FirstOrDefault().Extension
                }).ToList();
        }

        public bool IsProductInCart(int productId, string userId)
        {
            return this.db.ProductCarts.Any(x => x.ProductId == productId && x.UserId == userId);
        }

        public ProductCart GetProductFromCart(int productId, string userId)
        {
            return this.db.ProductCarts.FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
        }
        public List<ProductCart> GetAllProductsFromCart(string userId)
        {
            return this.db.ProductCarts.
                Where(x => x.UserId == userId)
                .ToList();
        }

        public async Task RemoveAllProductsWhenOrderIsCompleted(string userId)
        {
            var products = this.GetAllProductsFromCart(userId);
            foreach (var product in products)
            {
                product.IsDeleted = true;
            }
            await this.db.SaveChangesAsync();
        }

    }
}
