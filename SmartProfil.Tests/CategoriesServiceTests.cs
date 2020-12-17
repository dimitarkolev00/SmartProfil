using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Services;
using SmartProfil.ViewModels.InputModels;
using Xunit;

namespace SmartProfil.Tests
{
    public class CategoriesServiceTests
    {
        [Fact]
        public void AddAsyncShouldAddNewCategory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test");
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new CategoriesService(dbContext);

            var inputModel = new AddCategoryInputModel
            {
                Name = "test name"
            } ;

            var result = service.AddAsync(inputModel);

           Assert.True(dbContext.Categories.Count()==1);
        }

        [Fact]
        public void GetAllAsKeyValuePairsShouldReturnKeyValuePair()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test");
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new CategoriesService(dbContext);

            var result = service.GetAllAsKeyValuePairs();

            Assert.True(!result.Any());
        }
    }
}
