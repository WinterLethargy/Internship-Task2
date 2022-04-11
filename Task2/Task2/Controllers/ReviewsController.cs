using Microsoft.AspNetCore.Mvc;
using Task2.Models;
using Task2.Repositories;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Task2.Controllers
{
    [ApiController]
    [Route("games/reviews")]
    public class ReviewsController : Controller
    {
        private IReviewsRepository _reviewsRepository;

        public ReviewsController(IReviewsRepository reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }

        /// <summary>
        /// Добавляет рецензию на игру в базу данных
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="text">Текст необязателен</param>
        /// <param name="rating">Должен быть в пределах от 0 до 10</param>
        /// <returns></returns>
        /// <response code="400">
        /// Если рецензия ссылается на несуществующую игру, Id не приводится к Guid или рейтинг выходит за границы 0-10
        /// </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddReview([Required] string gameId, string? text, [Required] int rating)
        {
            Guid _gameId;
            try
            {
                _gameId = new Guid(gameId);
            }
            catch (FormatException ex)
            {
                return BadRequest();
            }

            var review = new Review() { GameId = _gameId, Text = text, Rating = rating };
            var reviewId = await _reviewsRepository.PostAsync(review);

            if (reviewId == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
