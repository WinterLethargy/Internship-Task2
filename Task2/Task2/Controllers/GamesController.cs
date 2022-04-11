using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        /// <response code="204">В базе данных нет игр</response>
        [HttpGet()]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Игры с таким Id нет в базе данных</response>
        /// <response code="400">Id не приводится к типу Guid</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGame([Required] string id)
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
        /// <response code="204">Передан несуществующий Id</response>
        /// <response code="400">Id не приводится к Guid</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteGame([Required] string id)
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
        /// <response code="400">Не может быть игры без имени или жанра</response>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddGame([Required] string name, [Required] string genr)
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
