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
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoriesService categoriesService;
        private readonly IManufacturersService manufacturersService;
        private readonly IProductMaterialTypesService productMaterialTypesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(IProductService productService,
            ICategoriesService categoriesService,
            IManufacturersService manufacturersService,
            IProductMaterialTypesService productMaterialTypesService,
            UserManager<ApplicationUser> userManager)
        {
            this.productService = productService;
            this.categoriesService = categoriesService;
            this.manufacturersService = manufacturersService;
            this.productMaterialTypesService = productMaterialTypesService;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var viewModel = new ProductInputModel
            {
                Categories = this.categoriesService.GetAllAsKeyValuePairs(),
                Manufacturers = this.manufacturersService.GetAllAsKeyValuePairs(),
                ProductMaterialTypes = this.productMaterialTypesService.GetAllAsKeyValuePairs()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductInputModel input)
        {
            if (!ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetAllAsKeyValuePairs();
                input.Manufacturers = this.manufacturersService.GetAllAsKeyValuePairs();
                input.ProductMaterialTypes = this.productMaterialTypesService.GetAllAsKeyValuePairs();

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.productService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                input.Categories = this.categoriesService.GetAllAsKeyValuePairs();
                input.Manufacturers = this.manufacturersService.GetAllAsKeyValuePairs();
                input.ProductMaterialTypes = this.productMaterialTypesService.GetAllAsKeyValuePairs();

                return this.View(input);
            }

            this.TempData["Message"] = "Product added successfully!";

            return this.RedirectToAction("All");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 1;

            var viewModel = new ProductsListViewModel
            {
                PageNumber = id,
                ProductsCount = this.productService.GetCount(),
                Products = this.productService.GetAll<ProductInListViewModel>(id, ItemsPerPage),
                ItemsPerPage = ItemsPerPage
            };
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var productViewModel = this.productService.GetById<SingleProductViewModel>(id);
            return this.View(productViewModel);
        }
    }
}
