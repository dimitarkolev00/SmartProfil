using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Moq;
using SmartProfil.AutoMapper;
using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Services;
using SmartProfil.ViewModels;
using Xunit;

namespace SmartProfil.Tests
{
    public class ProductServiceTemplateMethodsTest
    {
        [Fact]
        public void GetByIdShouldReturnProduct()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            AutoMapperConfig.RegisterMappings(typeof(SingleProductViewModelTest).Assembly);

            var product = new Product
            {
                Description = "test Description",
                Id = 2,
                IsDeleted = false,
                Name = "test"
            };

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var result = service.GetById<SingleProductViewModelTest>(2);

            Assert.Equal(2, result.Id);
            Assert.Equal("test Description", result.Description);
        }

        [Fact]
        public void GetAllWindowSillsShouldReturnAllProductFromWindowSillCategory()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            AutoMapperConfig.RegisterMappings(typeof(ProductInListViewModelTest).Assembly);
            var category = new Category
            {
                Id = 1,
                Name = "Window Sill"
            };
            var product = new Product
            {
                Id = 2,
                Name = "test",
                Model = "x",
                Category = category,
                IsDeleted = false,
                Description = "test Description",
            };

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var result = service.GetAllWindowSills<ProductInListViewModelTest>(1, 1);

            Assert.Single(result);
            Assert.Equal(2,result.FirstOrDefault(x=>x.Model=="x").Id);
        }

        [Fact]
        public void GetAllAccessoriesShouldReturnAllProductFromAccessoriesCategory()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            AutoMapperConfig.RegisterMappings(typeof(ProductInListViewModelTest).Assembly);
            var category = new Category
            {
                Id = 1,
                Name = "Accessories"
            };
            var product = new Product
            {
                Id = 4,
                Name = "test",
                Model = "x",
                Category = category,
                IsDeleted = false,
                Description = "test Description",
            };

            var category2 = new Category
            {
                Id = 11,
                Name = "Profile"
            };
            var product2 = new Product
            {
                Id = 3,
                Name = "test2",
                Model = "x2",
                Category = category2,
                IsDeleted = false,
                Description = "test Description2",
            };

            dbContext.Products.Add(product);
            dbContext.Products.Add(product2);
            dbContext.SaveChanges();

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var result = service.GetAllAccessories<ProductInListViewModelTest>(1, 1);

            Assert.Single(result);
            Assert.Equal(4, result.FirstOrDefault(x => x.Model == "x").Id);
        }

        [Fact]
        public void GetAllProfileShouldReturnAllProductFromProfileCategory()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            AutoMapperConfig.RegisterMappings(typeof(ProductInListViewModelTest).Assembly);
            var category = new Category
            {
                Id = 1,
                Name = "Profile"
            };
            var product = new Product
            {
                Id = 12,
                Name = "test",
                Model = "x",
                Category = category,
                IsDeleted = false,
                Description = "test Description",
            };

            var category2 = new Category
            {
                Id = 11,
                Name = "Profile"
            };
            var product2 = new Product
            {
                Id = 3,
                Name = "test2",
                Model = "x2",
                Category = category2,
                IsDeleted = false,
                Description = "test Description2",
            };

            dbContext.Products.Add(product);
            dbContext.Products.Add(product2);
            dbContext.SaveChanges();

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var result = service.GetAllProfiles<ProductInListViewModelTest>(1, 1);

            Assert.Single(result);
            Assert.Equal(12, result.FirstOrDefault(x => x.Model == "x").Id);
        }


        [Fact]
        public void GetAllShouldReturnAllProductFromAllCategoriesWithTwoItemsPerPage()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            AutoMapperConfig.RegisterMappings(typeof(ProductInListViewModelTest).Assembly);
            var category = new Category
            {
                Id = 1,
                Name = "Profile"
            };
            var product = new Product
            {
                Id = 12,
                Name = "test",
                Model = "x",
                Category = category,
                IsDeleted = false,
                Description = "test Description",
            };

            var category2 = new Category
            {
                Id = 11,
                Name = "Profile"
            };
            var product2 = new Product
            {
                Id = 3,
                Name = "test2",
                Model = "x2",
                Category = category2,
                IsDeleted = false,
                Description = "test Description2",
            };

            dbContext.Products.Add(product);
            dbContext.Products.Add(product2);
            dbContext.SaveChanges();

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var result = service.GetAll<ProductInListViewModelTest>(1, 2);

            Assert.Equal(2,result.Count());
            Assert.Equal(12, result.FirstOrDefault(x => x.Model == "x").Id);
        }
    }
    public class SingleProductViewModelTest : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ProductInListViewModelTest : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }
    }
}
