using System.Collections.Generic;
using System.Threading.Tasks;
using SmartProfil.Models;
using SmartProfil.ViewModels;

namespace SmartProfil.Services.Interfaces
{
    public interface ICartService
    {
        public bool IsProductInCart(int productId, string userId);

        public ProductCart GetProductFromCart(int productId, string userId);

        public Task RemoveProductByIdAsync(string userId, int productId);

        public Task AddToCartAsync(int productId, string userId, int quantity = 1);

        public List<ProductCartViewModel> GetAllProductsForCartViewModel(string userId);

        public List<ProductCartViewModel> GetAllPreviousOrders(string userId);

        public Task RemoveAllProductsWhenOrderIsCompleted(string userId);
    }
}
