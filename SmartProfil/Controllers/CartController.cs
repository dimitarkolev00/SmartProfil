using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartProfil.Models;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(ICartService cartService, IProductService productService,
            UserManager<ApplicationUser> userManager)
        {
            this.cartService = cartService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var currentUserId = user.Id;

            var productsInCart = this.cartService.GetAllProductsForCartViewModel(currentUserId);

            var model = new  AllCartProducts()
            {
                AllCartItems = productsInCart
            };

            return this.View("Cart", model);

            //var complexModel = new ComplexModel<List<BuyProductInputModel>, List<ProductCartViewModel>>
            //{
            //    ViewModel = productsInCart
            //};
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartInputModel inputModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var currentUserId = user.Id;

            
            var productId = inputModel.ProductId;
            var quantity = inputModel.Quantity;

            await this.cartService.AddToCartAsync(productId, currentUserId, quantity);

            this.TempData["Message"] = "New item added to cart successfully!";

            return this.RedirectToAction(nameof(this.All));

        }

    }
}
