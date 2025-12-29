using Microsoft.EntityFrameworkCore;
using MvcPustokTask.Models;
using System.Collections.Generic;

namespace MvcPustokTask.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> optionsBuilder) : base(optionsBuilder)
        {
        }

        public DbSet<FeatureProduct> FeatureProducts { get; set; }
    }
}
