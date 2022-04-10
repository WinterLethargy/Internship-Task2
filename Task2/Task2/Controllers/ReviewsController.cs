using Microsoft.AspNetCore.Mvc;
using Task2.Models;
using Task2.Repositories;

namespace Task2.Controllers
{
    [Route("games/reviews")]
    public class ReviewsController : Controller
    {
        private IReviewsRepository _reviewsRepository;

        public ReviewsController(IReviewsRepository reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(string gameId, string text, int rating)
        {
            Guid _gameId;
            try
            {
                _gameId = new Guid(gameId);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }

            var review = new Review() { GameId = _gameId, Text = text, Rating = rating };
            var succes = await _reviewsRepository.PostAsync(review);

            if (succes == string.Empty)
                return Ok("рецензия добавлена");
            else
                return BadRequest(succes);
        }
    }
}
