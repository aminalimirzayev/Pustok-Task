using Microsoft.AspNetCore.Mvc;

namespace MvcPustokTask.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
