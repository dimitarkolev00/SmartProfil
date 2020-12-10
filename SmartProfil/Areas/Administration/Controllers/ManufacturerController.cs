using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturersService manufacturersService;
        private readonly IProductService productService;

        public ManufacturerController(IManufacturersService manufacturersService, IProductService productService)
        {
            this.manufacturersService = manufacturersService;
            this.productService = productService;
        }
        public IActionResult AddNew()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AddManufacturerInputModel inputModel)
        {
            await this.manufacturersService.AddAsync(inputModel);

            this.TempData["Message"] = "New manufacturer added successfully!";

            return this.Redirect("/Products/Create");
        }

    }
}
