using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IManufacturersService manufacturersService;

        public ManufacturerController(IManufacturersService manufacturersService)
        {
            this.manufacturersService = manufacturersService;
        }
        public  IActionResult AddNew()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AddManufacturerInputModel inputModel)
        {
            await this.manufacturersService.AddAsync(inputModel);
            return this.Redirect("/Products/Create");
        }
    }
}
