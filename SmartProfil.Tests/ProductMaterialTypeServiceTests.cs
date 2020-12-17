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
    public class ProductMaterialTypeServiceTests
    {
        [Fact]
        public void AddAsyncShouldAddNewCategory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test");
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ProductMaterialTypesService(dbContext);

            var inputModel = new AddProductMaterialTypeInputModel
            {
                Name = "test name",
            };

            var result = service.AddAsync(inputModel);

            Assert.True(dbContext.ProductMaterialTypes.Count() == 1);
        }

        [Fact]
        public void GetAllAsKeyValuePairsShouldReturnKeyValuePair()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test");
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new ProductMaterialTypesService(dbContext);

            var result = service.GetAllAsKeyValuePairs();

            Assert.True(!result.Any());
        }
    }
}
