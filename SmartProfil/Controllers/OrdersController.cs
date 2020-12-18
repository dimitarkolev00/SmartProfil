using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartProfil.Models;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrdersService ordersService;
        private readonly ICartService cartService;

        public OrdersController(UserManager<ApplicationUser> userManager,
            IOrdersService ordersService,
            ICartService cartService)
        {
            this.userManager = userManager;
            this.ordersService = ordersService;
            this.cartService = cartService;
        }

        [Authorize]
        public IActionResult OrderForm()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> OrderForm(OrderFormInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.ordersService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.View(input);
            }

            await this.cartService.RemoveAllProductsWhenOrderIsCompleted(user.Id);

            this.TempData["Message"] = "Order successfully completed !";

            return this.RedirectToAction("Index","Home");
        }

        [Authorize]
        public async Task<IActionResult> GetPreviousOrders()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;

            var productsInCart = this.cartService.GetAllPreviousOrders(userId);

            var model = new AllCartProducts()
            {
                AllCartItems = productsInCart
            };

            return this.View("PreviousOrders",model);
        }
    }
}
