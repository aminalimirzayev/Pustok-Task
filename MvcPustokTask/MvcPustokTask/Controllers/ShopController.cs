using Microsoft.AspNetCore.Mvc;

namespace MvcPustokTask.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
