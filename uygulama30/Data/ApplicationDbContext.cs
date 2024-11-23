using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using uygulama30.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace uygulama30.Context
{
    public class ApplicationDbContext :IdentityDbContext<AppUser,AppRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }  
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
