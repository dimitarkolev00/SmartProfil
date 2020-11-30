using System.Threading.Tasks;

namespace SmartProfil.Services.Interfaces
{
    public interface IFeedbacksService
    {
        Task SetRatingAsync(int productId, string userId, int rating);

        double GetAverageRating(int productId);
    }
}
