using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using SmartProfil.Data;
using SmartProfil.Services;
using SmartProfil.ViewModels.InputModels;
using Xunit;

namespace SmartProfil.Tests
{
    public class ManufacturerServiceTests
    {
        [Fact]
        public void AddAsyncShouldAddNewCategory()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test");
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ManufacturersService(dbContext , mockEnvironment.Object);

            var inputModel = new AddManufacturerInputModel
            {
                Country = "Germany",
                Name = "test name",
                Images = new List<IFormFile>()
            };

            var result = service.AddAsync(inputModel);

            Assert.True(dbContext.Manufacturers.Count() == 1);
        }

        [Fact]
        public void GetAllAsKeyValuePairsShouldReturnKeyValuePair()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test");
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ManufacturersService(dbContext , mockEnvironment.Object);

            var result = service.GetAllAsKeyValuePairs();

            Assert.True(!result.Any());
        }

    }
}
