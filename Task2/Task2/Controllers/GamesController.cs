using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.Models;
using Task2.Repositories;

namespace Task2.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : Controller
    {
        private IGamesRepository _gameRepository;

        public GamesController(IGamesRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        /// <summary>
        /// Возвращает список всех игр без рецензий
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet()]
        public async Task<IActionResult> GetAllGames()
        {
            var games = await _gameRepository.GetAllAsync();
            
            if (games == null)
            {
                return NoContent();
            }

            return Json(games);
        }

        /// <summary>
        /// Возвращает игру и список ее рецензий
        /// </summary>
        /// <param name="id">Игра с таким Id должна существовать</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame(string id)
        {
            Guid gameId;
            try
            {
                gameId = new Guid(id);
            }
            catch (FormatException ex)
            {
                return BadRequest();
            }

            var game = await _gameRepository.GetAsync(gameId);

            if (game == null)
            {
                return NoContent();
            }

            return Json(game);
        }

        /// <summary>
        /// Удаляет игру и ее рецензии из базы данных
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(string id)
        {
            Guid gameId;
            try
            {
                gameId = new Guid(id);
            }
            catch (FormatException ex)
            {
                return BadRequest();
            }

            var result = await _gameRepository.DeleteAsync(gameId);
            
            if (!result)
            {
                return NoContent();
            }
                
            return Ok();
        }

        /// <summary>
        /// Добавляет игру в базу данных
        /// </summary>
        /// <param name="name"></param>
        /// <param name="genr"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [HttpPost()]
        public async Task<IActionResult> AddGame(string name, string genr)
        {
            var game = new Game() { Name = name, Genre = genr };
            var gameId = await _gameRepository.PostAsync(game);

            if (gameId == null)
            {
                return BadRequest();
            }
            
            return Ok();
        }
    }
}
