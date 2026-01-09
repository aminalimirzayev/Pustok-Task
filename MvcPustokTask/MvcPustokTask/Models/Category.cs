using System.ComponentModel.DataAnnotations.Schema;

namespace MvcPustokTask.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; } 
        [NotMapped]
        public IFormFile? ImageFile { get; set; } 
    }
}