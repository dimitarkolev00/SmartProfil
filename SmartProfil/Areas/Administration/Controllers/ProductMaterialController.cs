using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Areas.Administration.Controllers
{
    
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class ProductMaterialController:Controller
    {
        private readonly IProductMaterialTypesService productMaterialTypesService;

        public ProductMaterialController(IProductMaterialTypesService productMaterialTypesService)
        {
            this.productMaterialTypesService = productMaterialTypesService;
        }
        public IActionResult AddNew()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AddProductMaterialTypeInputModel inputModel)
        {
            await this.productMaterialTypesService.AddAsync(inputModel);

            this.TempData["Message"] = "New material type added successfully!";

            return this.Redirect("/Products/Create");
        }
    }
}
