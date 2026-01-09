using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustokTask.DAL;
using System.Threading.Tasks;

namespace MvcPustokTask.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ProductCount = await _context.Products.CountAsync();
            ViewBag.CategoryCount = await _context.Categories.CountAsync();
            return View();
        }
    }
}