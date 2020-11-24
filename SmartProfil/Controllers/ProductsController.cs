using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ProductsController(IProductService productService,
            ICategoriesService categoriesService,
            IManufacturersService manufacturersService,
            IProductMaterialTypesService productMaterialTypesService)
        {
            this.productService = productService;
            this.categoriesService = categoriesService;
            this.manufacturersService = manufacturersService;
            this.productMaterialTypesService = productMaterialTypesService;
        }

        public IActionResult Add()
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
        public IActionResult Add(ProductInputModel input)
        {
            if (!ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetAllAsKeyValuePairs();
                input.Manufacturers = this.manufacturersService.GetAllAsKeyValuePairs();
                input.ProductMaterialTypes = this.productMaterialTypesService.GetAllAsKeyValuePairs();

                return this.View(input);
            }

            this.productService.Add(input);

            return this.Redirect("/");
        }
    }
}
