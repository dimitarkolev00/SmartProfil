using System;
using System.Threading.Tasks;
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

            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new ProductsListViewModel
            {
                PageNumber = id,
                Products = this.productService.GetAll(id, 12),
            };
            return this.View(viewModel);
        }
    }
}
