using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class CategoryController:Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }
        public IActionResult AddNew()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AddCategoryInputModel inputModel)
        {
            await this.categoriesService.AddAsync(inputModel);

            this.TempData["Message"] = "New category added successfully!";

            return this.Redirect("/Products/Create");
        }
    }
}
