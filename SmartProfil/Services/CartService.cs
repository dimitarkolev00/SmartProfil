using System;
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
                    Quantity = quantity

                });
            }

            await this.db.SaveChangesAsync();
        }

        public List<ProductCartViewModel> GetAllProductsForCartViewModel(string userId)
        {
            return this.db.ProductCarts
                .Where(x => x.UserId == userId)
                .Select(x => new ProductCartViewModel
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Model = x.Product.Model,
                    ManufacturerName = x.Product.Manufacturer.Name,
                    Quantity = x.Quantity,
                    SinglePrice = x.Product.UnitPrice

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

    }
}
