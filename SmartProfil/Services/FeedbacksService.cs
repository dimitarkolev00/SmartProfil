using System.Linq;
using System.Threading.Tasks;
using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Services.Interfaces;

namespace SmartProfil.Services
{
    public class FeedbackService : IFeedbacksService
    {
        private readonly ApplicationDbContext db;

        public FeedbackService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task SetRatingAsync(int productId, string userId, int rating)
        {
            var feedback = this.db.Feedbacks
                .FirstOrDefault(x => x.Product.Id == productId && x.User.Id == userId);

            if (feedback==null)
            {
                feedback = new Feedback
                {
                     ProductId = productId,
                     UserId = userId,
                };

                await this.db.Feedbacks.AddAsync(feedback);
            }

            feedback.Rating = rating;

            await this.db.SaveChangesAsync();
        }

        public double GetAverageRating(int productId)
        {
            return this.db.Feedbacks
                .Where(x => x.ProductId == productId)
                .Average(x => x.Rating);
        }
    }
}
