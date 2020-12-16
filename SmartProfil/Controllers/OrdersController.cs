using Microsoft.AspNetCore.Mvc;

namespace SmartProfil.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult OrderForm()
        {
            return this.View();
        }
    }
}
