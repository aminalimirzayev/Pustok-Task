using MvcPustokTask.Models;
using System.Collections.Generic;

namespace MvcPustokTask.ViewModels
{
    public class HomeVM
    {
        public List<HeroArea> Areas { get; set; }

       
        public List<FeatureProduct> FeatureProducts { get; set; }

        public List<Product> NewProducts { get; set; }
        public List<Product> CheapProducts { get; set; }
    }
}