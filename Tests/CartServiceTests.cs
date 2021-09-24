using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            dbContext.ProductCarts.Add(productCart);
            dbContext.SaveChanges();

            var service = new CartService(dbContext);

            var result = service.GetProductFromCart(2, "miti");

            Assert.NotNull(result);
            Assert.Equal("miti", result.UserId);
            Assert.Equal("test Description", result.Product.Description);
        }

        [Fact]
        public void GetProductFromCartShouldReturnNullIfProductDoesNotExists()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            var service = new CartService(dbContext);

            var result = service.GetProductFromCart(2, "mitko");

            Assert.Null(result);
        }

        [Fact]
        public void RemoveAllProductsWhenOrderIsCompletedShouldRemoveOrders()
        {
            var productCart = new ProductCart()
            {
                ProductId = 3,
                Quantity = 2,
                UserId = "dimitar"
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            dbContext.ProductCarts.Add(productCart);
            dbContext.SaveChanges();

            var service = new CartService(dbContext);

            var result = service.RemoveAllProductsWhenOrderIsCompleted("dimitar");

            Assert.Equal(true, dbContext.ProductCarts.FirstOrDefault(x => x.UserId == "dimitar").IsDeleted);
        }



        [Fact]
        public void IsProductInCartShouldReturnTrueIfProductIsInCart()
        {
            var productCart = new ProductCart()
            {
                ProductId = 3,
                Quantity = 2,
                UserId = "mitko2"
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            dbContext.ProductCarts.Add(productCart);
            dbContext.SaveChanges();

            var service = new CartService(dbContext);

            var result = service.IsProductInCart(3, "mitko2");

            Assert.True(result);
        }

        [Fact]
        public void IsProductInCartShouldReturnFalseIfProductIsNotInCart()
        {

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new CartService(dbContext);

            var result = service.IsProductInCart(4, "mitko2");

            Assert.False(result);
        }

        [Fact]
        public void RemoveProductByIdAsyncShouldRemoveIfProductExists()
        {
            var productCart = new ProductCart()
            {
                ProductId = 3,
                Quantity = 2,
                UserId = "dimitar"
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            dbContext.ProductCarts.Add(productCart);
            dbContext.SaveChanges();

            var service = new CartService(dbContext);

            var result = service.RemoveProductByIdAsync("dimitar", 3);

            Assert.False(dbContext.ProductCarts.Any());
        }

        [Fact]
        public void RemoveProductByIdAsyncShouldNotRemoveIfProductDoesNotExists()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new CartService(dbContext);

            var result = service.RemoveProductByIdAsync("dimitar", 3);

            Assert.True(result.Status == TaskStatus.Faulted);
        }

        [Fact]
        public void AddToCartAsyncShouldAddToCartIfProductIsNotInCartYet()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new CartService(dbContext);

            var result = service.AddToCartAsync(12, "kris", 10);

            Assert.True(dbContext.ProductCarts.Any(x => x.UserId == "kris"));
        }

        [Fact]
        public void AddToCartAsyncShouldIncreaseProductQuantityIfProductIsAlreadyAddedToCart()
        {
            var productCart = new ProductCart()
            {
                ProductId = 3,
                Quantity = 2,
                UserId = "dimitar"
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            dbContext.ProductCarts.Add(productCart);
            dbContext.SaveChanges();

            var service = new CartService(dbContext);

            var product = service.GetProductFromCart(3, "dimitar");

            Assert.Equal(2, product.Quantity);
        }

        [Fact]
        public void GetAllProductsForCartViewModelShouldNotReturnIfProductIsNotFound()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            dbContext.SaveChanges();

            var service = new CartService(dbContext);

            var result = service.GetAllProductsForCartViewModel("dimitar");

            Assert.True(result.Count == 0);
        }

        [Fact]
        public void GetAllPreviousOrdesShouldNotReturnIfProductIsNotFound()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            dbContext.SaveChanges();

            var service = new CartService(dbContext);

            var result = service.GetAllPreviousOrders("dimitar");

            Assert.True(result.Count == 0);
        }
    }
}
