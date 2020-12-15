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
        private readonly IProductService productService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(ICartService cartService, IProductService productService,
            UserManager<ApplicationUser> userManager)
        {
            this.cartService = cartService;
            this.productService = productService;
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
            //var complexModel = new ComplexModel<List<BuyProductInputModel>, List<ProductCartViewModel>>
            //{
            //    ViewModel = productsInCart
            //};

            //if (TempData.ContainsKey(GlobalConstants.ErrorsFromPOSTRequest))
            //{
            //    //Merge model states
            //    ModelStateHelper.MergeModelStates(TempData, this.ModelState);
            //}

            //if (this.TempData[GlobalConstants.InputModelFromPOSTRequestType]?.ToString() == $"List<{nameof(BuyProductInputModel)}>")
            //{
            //    complexModel.InputModel = JsonSerializer.Deserialize<List<BuyProductInputModel>>(this.TempData[GlobalConstants.InputModelFromPOSTRequest]?.ToString());
            //}

            return this.View("Cart", model);
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

            return this.RedirectToAction(nameof(this.All));

            //var product = this.productService.GetProductById(productId);


            //if (product.QuantityInStock < quantity)
            //{
            //    this.ModelState.AddModelError("QuantityInStock", "Cannot buy more of this product than there is in stock");
            //}

            //if (this.ModelState.IsValid == false)
            //{
            //    //Store needed info for get request in TempData only if the model state is invalid after doing the complex checks
            //    TempData[GlobalConstants.ErrorsFromPOSTRequest] = ModelStateHelper.SerialiseModelState(this.ModelState);

            //    //Store needed info for get request in TempData
            //    return this.RedirectToAction("ProductPage", "Products", new { productId = productId }, "Reviews");
            //}
        }


        //public async Task<IActionResult> AddToCart()
        //{
        //    var user = await this.userManager.GetUserAsync(this.User);


        //    var cart = this.cartService.GetCartByUserId(user.Id.ToString());

        //    var cartModel = new Cart
        //    {
        //        CartItems = this.cartService.GetCartItems(2),
        //        UserId = user.Id.ToString(),
        //        Id = cart.Id
        //    };

        //    return this.View("Cart", cartModel);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddToCart(int productId, int quantity)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return this.Redirect("/Identity/Account/Login");
        //    }

        //    var user = await this.userManager.GetUserAsync(this.User);

        //    await this.cartService.AddItem(user.Id, productId, quantity);

        //    var cart = this.cartService.GetCartByUserId(user.Id.ToString());

        //    var cartModel = new Cart
        //    {
        //        CartItems = this.cartService.GetCartItems(2),
        //        UserId = user.Id,
        //        User = user,
        //        Id= cart.Id
        //    };

        //    return this.View("Cart", cartModel);
        //}
    }
}
