using Microsoft.AspNetCore.Mvc;

namespace MvcPustokTask.Areas.admin.Controllers
{
    public class CategoryController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}