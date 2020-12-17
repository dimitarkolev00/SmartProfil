using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartProfil.Data;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels;

namespace SmartProfil.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ApplicationDbContext db;
        private readonly IProductService productService;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext db,
            IProductService productService)
        {
            this.logger = logger;
            this.db = db;
            this.productService = productService;
        }
        public IActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                RandomProducts = this.productService.GetRandom<IndexPageProductViewModel>(4)
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
