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
        public DbSet<Image> Images { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ProductMaterialType> ProductMaterialTypes { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Feedback>()
        //        .HasOne(x=>x.User)
        //        .WithMany(a => a.Feedbacks)
        //        .OnDelete(DeleteBehavior.NoAction);
        //}

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<Image>()
    //            .HasOne(x => x.Product)
    //            .WithMany(a => a.Images)
    //            .OnDelete(DeleteBehavior.NoAction);
    //    }
    }
}
