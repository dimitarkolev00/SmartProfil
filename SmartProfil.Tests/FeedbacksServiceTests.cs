using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartProfil.Data;
using SmartProfil.Services;
using Xunit;

namespace SmartProfil.Tests
{
    public class FeedbacksServiceTests
    {
        [Fact]
        public async Task WhenUsersVoteCorrectCountShouldBeReturned()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new FeedbackService(dbContext);
         
            await service.SetRatingAsync(1, "1", 1);
            await service.SetRatingAsync(1, "1", 5);

            Assert.Equal(2, dbContext.Feedbacks.Count());
        }

        [Fact]
        public async Task WhenUsersVoteForTheSameRecipeTheAverageVoteShouldBeCorrect()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var service = new FeedbackService(dbContext);

            await service.SetRatingAsync(2, "Miti", 5);
            await service.SetRatingAsync(2, "Pesho", 2);
            await service.SetRatingAsync(2, "Kiro", 2);

            Assert.Equal(3, dbContext.Feedbacks.Count());
            Assert.Equal(3, service.GetAverageRating(2));
        }
    }
}
