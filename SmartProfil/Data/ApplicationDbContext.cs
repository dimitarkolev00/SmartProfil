using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartProfil.Models;

namespace SmartProfil.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<PreviousOrders> PreviousOrders { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<WishList> WishLists { get; set; }

        public DbSet<ProductMaterialType> ProductMaterialTypes { get; set; }

        public DbSet<ProductCart> ProductCarts { get; set; }

        public DbSet<OrderFormInfo> OrderFormInfo { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCart>()
                .HasKey(x => new { x.ProductId, x.UserId });

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(x => x.UserId);
        }
    }
}
