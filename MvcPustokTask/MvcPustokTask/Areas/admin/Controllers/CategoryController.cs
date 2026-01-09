using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustokTask.DAL;
using MvcPustokTask.Models;

namespace MvcPustokTask.Areas.admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

     
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            Category? category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            
            ModelState.Remove("Products");

            if (category.ImageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + category.ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "admin", "img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await category.ImageFile.CopyToAsync(stream);
                }
                category.ImageUrl = fileName;
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            Category? category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null || id != category.Id) return BadRequest();

            Category? existed = await _context.Categories.FindAsync(id);
            if (existed == null) return NotFound();

            ModelState.Remove("Products");
            ModelState.Remove("ImageFile"); 

            if (category.ImageFile != null)
            {
                
                if (!string.IsNullOrEmpty(existed.ImageUrl))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "admin", "img", existed.ImageUrl);
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + category.ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "admin", "img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await category.ImageFile.CopyToAsync(stream);
                }
                existed.ImageUrl = fileName;
            }

            if (!ModelState.IsValid)
            {
                return View(existed);
            }

            existed.Name = category.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            Category? category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            if (!string.IsNullOrEmpty(category.ImageUrl))
            {
                string path = Path.Combine(_env.WebRootPath, "admin", "img", category.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}