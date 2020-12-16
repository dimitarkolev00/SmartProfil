using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartProfil.Controllers;
using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Services;
using Xunit;

namespace SmartProfil.Tests
{
    public class CartServiceTests
    {
        [Fact]
        public void GetProductFromCartShouldReturnProductIfFound()
        {
            var product = new Product
            {
                Description = "test Description",
                Id = 2,
            };
            var productCart = new ProductCart()
            {
                ProductId = 2,
                Product = product,
                Quantity = 2,
                UserId = "miti"
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test");
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            dbContext.ProductCarts.Add(productCart);
            dbContext.SaveChanges();

            var service = new CartService(dbContext);

            var result = service.GetProductFromCart(2,"miti");

            Assert.NotNull(result);
            Assert.Equal("miti", result.UserId);
            Assert.Equal("test Description", result.Product.Description);
        }

        [Fact]
        public void GetProductFromCartShouldReturnNullIfProductDoesNotExists()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test");
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new CartService(dbContext);

            var result = service.GetProductFromCart(2,"mitko");

            Assert.Null(result);
          
        }
    }
}
