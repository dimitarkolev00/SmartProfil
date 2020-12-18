using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartProfil.Data;
using SmartProfil.Services;
using SmartProfil.ViewModels.InputModels;
using Xunit;

namespace SmartProfil.Tests
{
    public class OrdersServiceTests
    {
        [Fact]
        public void CreateAsyncShouldAddNewProduct()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new OrdersService(dbContext);

            var inputModel = new OrderFormInputModel()
            {
                FullName = "Mitko Kolev",
                ExpYear = "2021",
                Email = "dimitarkolev00@gmail.com",
                NameOnCard = "Dimitar Kolev",
                ExpMonth = "11",
                CVV = "123",
                CompanyName = "ProfiLink",
                City = "Sofia",
                Address = "Aadsfdasf",
                CardNumber = "0932098736472342"
            };
            var inputModel2 = new OrderFormInputModel()
            {
                FullName = "Mitko Kolev",
                ExpYear = "2021",
                Email = "dimitarkolev00@gmail.com",
                NameOnCard = "Dimitar Kolev",
                ExpMonth = "11",
                CVV = "123",
                CompanyName = "ProfiLink",
                City = "Sofia",
                Address = "Aadsfdasf",
                CardNumber = "0932098736472342"
            };

            var result = service.CreateAsync(inputModel, "mitko");
            result = service.CreateAsync(inputModel2, "kiro");

            Assert.True(dbContext.OrderFormInfo.Count() == 2);
        }
    }
}
