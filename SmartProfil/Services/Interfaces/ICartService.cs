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

        //public bool ProductIsInCart(int productId, string userId);

        public ProductCart GetProductFromCart(int productId, string userId);

        public List<ProductCartViewModel> GetAllProductsForCartViewModel(string userId);

        //Task<Cart> GetCartByUserId(string userId);
        //Task AddItem(string userId, int productId,int quantity);
        //Task RemoveItem(int cartId, int cartItemId);
        //Task ClearCart(string userName);

        //ICollection<CartItem> GetCartItems(int cartId);
    }
}
