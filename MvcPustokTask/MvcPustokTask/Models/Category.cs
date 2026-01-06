using Microsoft.AspNetCore.Mvc;

namespace MvcPustokTask.Models
{
    public class Category : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
