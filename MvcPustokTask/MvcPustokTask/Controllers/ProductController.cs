using Microsoft.AspNetCore.Mvc;

namespace MvcPustokTask.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
