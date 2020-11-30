using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels;

namespace SmartProfil.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbacksController : Controller
    {
        private readonly IFeedbacksService feedbacksService;

        public FeedbacksController(IFeedbacksService feedbacksService)
        {
            this.feedbacksService = feedbacksService;
        }

        public async Task<ActionResult<PostFeedbackResponseModel>> Post(int productId, int rating)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.feedbacksService.SetRatingAsync(productId, userId, rating);

            var averageRating = this.feedbacksService.GetAverageRating(productId);

            return new PostFeedbackResponseModel { AverageRating = averageRating };
        }
    }
}
