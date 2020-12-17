using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartProfil.Controllers
{
    public class OrdersController : Controller
    {
        [Authorize]
        public IActionResult OrderForm()
        {
            return this.View();
        }
    }
}
