using Microsoft.AspNetCore.Mvc;

namespace MvcPustokTask.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
