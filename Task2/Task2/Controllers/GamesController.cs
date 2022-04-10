using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.Models;
using Task2.Repositories;

namespace Task2.Controllers
{
    [Route("games")]
    public class GamesController : Controller
    {
        private IGamesRepository _gameRepository;

        public GamesController(IGamesRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllGames()
        {
            var games = await _gameRepository.GetAllAsync();
            
            if (games != null)
            {
                return Json(games);
            }
            else
            {
                return NoContent();
            }
        }

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
                return BadRequest(ex.Message);
            }

            var game = await _gameRepository.GetAsync(gameId);
            
            if(game != null)
            {
                return Json(game);
            }
            else
            {
                return NoContent();
            }
        }

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
                return BadRequest(ex.Message);
            }

            var result = await _gameRepository.DeleteAsync(gameId);
            if (result)
            {
                return Ok("Игра удалена");
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost()]
        public async Task<IActionResult> AddGame(string name, string genr)
        {
            var game = new Game() { Name = name, Genre = genr };
            var succes = await _gameRepository.PostAsync(game);

            if (succes == string.Empty)
                return Ok("Игра добавлена");
            else
                return BadRequest(succes);
        }
    }
}
