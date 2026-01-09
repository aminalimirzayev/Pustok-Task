namespace MvcPustokTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; } 
        public decimal Price { get; set; }
        public decimal? ExPrice { get; set; } 
        public int Discount { get; set; }
        public string Image { get; set; } 
        public string HoverImage { get; set; } 

        
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        
        public bool IsFeatured { get; set; } 
        public bool IsNew { get; set; } 
    }
}