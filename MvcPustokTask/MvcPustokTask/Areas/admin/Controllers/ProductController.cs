using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustokTask.DAL;
using MvcPustokTask.Models;

namespace MvcPustokTask.Areas.admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.Include(p => p.Category).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile ImageFile)
        {
            
            ModelState.Remove("Category");
            ModelState.Remove("Image");
            ModelState.Remove("HoverImage");
            ModelState.Remove("Description");

            if (ImageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "admin", "img", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
                product.Image = fileName;
                product.HoverImage = fileName;
            }

            
            if (string.IsNullOrEmpty(product.Description)) product.Description = "No description provided.";

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(product);
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Product product, IFormFile ImageFile)
        {
            if (id == null || id != product.Id) return BadRequest();

            var existed = await _context.Products.FindAsync(id);
            if (existed == null) return NotFound();

         
            ModelState.Remove("Category");
            ModelState.Remove("Image");
            ModelState.Remove("HoverImage");
            ModelState.Remove("Description");

            if (ImageFile != null)
            {
                if (!string.IsNullOrEmpty(existed.Image))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "admin", "img", existed.Image);
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }
                string fileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "admin", "img", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
                existed.Image = fileName;
                existed.HoverImage = fileName;
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(product);
            }

            existed.Name = product.Name;
            existed.Description = string.IsNullOrEmpty(product.Description) ? "No description" : product.Description;
            existed.Author = product.Author;
            existed.Price = product.Price;
            existed.ExPrice = product.ExPrice;
            existed.Discount = product.Discount;
            existed.CategoryId = product.CategoryId;
            existed.IsFeatured = product.IsFeatured;
            existed.IsNew = product.IsNew;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            if (!string.IsNullOrEmpty(product.Image))
            {
                string path = Path.Combine(_env.WebRootPath, "admin", "img", product.Image);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}