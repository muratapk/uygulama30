using eticaretapi.Models;
using Microsoft.EntityFrameworkCore;

namespace eticaretapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
        }
        public DbSet<Category>? categories { get; set; }
        public DbSet<Product>? products { get; set; }
    }
}
