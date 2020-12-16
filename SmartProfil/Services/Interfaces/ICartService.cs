using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartProfil.Models;
using SmartProfil.ViewModels;

namespace SmartProfil.Services.Interfaces
{
    public interface ICartService
    {

        public Task AddToCartAsync(int productId, string userId, int quantity = 1);

        public bool IsProductInCart(int productId, string userId);

        public ProductCart GetProductFromCart(int productId, string userId);

        public List<ProductCartViewModel> GetAllProductsForCartViewModel(string userId);

    }
}
