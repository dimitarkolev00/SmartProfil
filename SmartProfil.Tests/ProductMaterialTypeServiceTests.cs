using System.Linq;
using Microsoft.EntityFrameworkCore;
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
