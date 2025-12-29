using MvcPustokTask.Models;

namespace MvcPustokTask.ViewModels
{
    public class HomeVM
    {
        public List<Arrials> Products { get; set; }
        public List<Product> Product { get; set; }
        public List<FeatureProduct> FeatureProducts { get; set; }

    }
}
