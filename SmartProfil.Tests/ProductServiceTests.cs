using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Services;
using SmartProfil.ViewModels.InputModels;
using Xunit;

namespace SmartProfil.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public void CreateAsyncShouldAddNewProduct()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var inputModel = new ProductInputModel()
            {
                Description = "test descr",
                Name = "test name",
                Images = new List<IFormFile>(),
            };
            var inputModel2 = new ProductInputModel()
            {
                Description = "test descr",
                Name = "test name",
                Images = new List<IFormFile>(),
            };

            var result = service.CreateAsync(inputModel,"mitko"); 
            result = service.CreateAsync(inputModel2,"kiro");

            Assert.True(dbContext.Products.Count() == 2);
        }

        [Fact]
        public async void GetCountShouldReturnCorrectCountOfProductsAndCorrectProduct()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var product = new Product
            {
                Description = "test descr",
                Images = new List<Image>(),
                CategoryId = 1
            };
            var product2 = new Product
            {
                Description = "test descr2",
                Images = new List<Image>(),
                CategoryId = 2
            };
            await dbContext.Products.AddAsync(product);
            await dbContext.Products.AddAsync(product2);
            await dbContext.SaveChangesAsync();

            var result = service.GetCount();

            Assert.Equal(result,dbContext.Products.Count());
            Assert.Equal("test descr",dbContext.Products.FirstOrDefault(x=>x.CategoryId==1).Description);
        }

        [Fact]
        public void GetCountShouldReturnNullIfProductIsNotFound()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var result = service.GetCount();

            Assert.Equal(result, dbContext.Products.Count());
        }

        [Fact]
        public async void GetProfilesCountShouldReturnCorrectCountOfProductsAndCorrectProductInProfileCategory()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var category = new Category
            {
                Name = "Profile",
                Id = 1
            };
            var product = new Product
            {
                Description = "test descr",
                Images = new List<Image>(),
                Category = category
            };

            var category2 = new Category
            {
                Name = "Accessories",
                Id = 1
            };
            var product2 = new Product
            {
                Description = "test descr2",
                Images = new List<Image>(),
                Category = category
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.Products.AddAsync(product2);
            await dbContext.SaveChangesAsync();

            var result = service.GetProfilesCount();

            Assert.Equal(result, dbContext.Products.Count(x => x.Category.Name == "Profile"));
            Assert.Equal("test descr", dbContext.Products.FirstOrDefault(x => x.CategoryId == 1).Description);
        }

        [Fact]
        public void GetProfileCountShouldReturnNullIfProductIsNotFound()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var result = service.GetProfilesCount();

            Assert.Equal(result, dbContext.Products.Count());
        }

        [Fact]
        public async void GetAccessoriesCountShouldReturnCorrectCountOfProductsAndCorrectProductInAccessoriesCategory()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var category = new Category
            {
                Name = "Accessories",
                Id = 1
            };
            var product = new Product
            {
                Description = "test descr",
                Images = new List<Image>(),
                Category = category
            };

            var category2 = new Category
            {
                Name = "Profile",
                Id = 1
            };
            var product2 = new Product
            {
                Description = "test descr2",
                Images = new List<Image>(),
                Category = category
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.Products.AddAsync(product2);
            await dbContext.SaveChangesAsync();

            var result = service.GetAccessoriesCount();

            Assert.Equal(result, dbContext.Products.Count(x => x.Category.Name == "Accessories"));
            Assert.Equal("test descr", dbContext.Products.FirstOrDefault(x => x.CategoryId == 1).Description);
        }

        [Fact]
        public void GetAccessoriesCountShouldReturnNullIfProductIsNotFound()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var result = service.GetAccessoriesCount();

            Assert.Equal(result, dbContext.Products.Count());
        }


        [Fact]
        public async void GetWindowsillsCountShouldReturnCorrectCountOfProductsAndCorrectProductInWindowSillCategory()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var category = new Category
            {
                Name = "Window Sill",
                Id = 1
            };
            var product = new Product
            {
                Description = "test descr",
                Images = new List<Image>(),
                Category = category
            };

            var category2 = new Category
            {
                Name = "Profile",
                Id = 1
            };
            var product2 = new Product
            {
                Description = "test descr2",
                Images = new List<Image>(),
                Category = category
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.Products.AddAsync(product2);
            await dbContext.SaveChangesAsync();

            var result = service.GetWindowSillsCount();

            Assert.Equal(result, dbContext.Products.Count(x => x.Category.Name == "Window Sill"));
            Assert.Equal("test descr", dbContext.Products.FirstOrDefault(x => x.Category.Name=="Window Sill").Description);
        }

        [Fact]
        public void GetWindosSillsCountShouldReturnNullIfProductIsNotFound()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ProductService(dbContext, mockEnvironment.Object);

            var result = service.GetWindowSillsCount();

            Assert.Equal(result, dbContext.Products.Count());
        }

    }
}
