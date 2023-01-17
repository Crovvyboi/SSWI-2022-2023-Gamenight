using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private ILogger<GameController> _logger;
        private IGameRepository _repo;

        public GameController(ILogger<GameController> logger, IGameRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var gameslist = _repo.GetAll;

                if (gameslist is null)
                {
                    return BadRequest("'GetAll returned null.");
                }
                _logger.LogInformation("Returned all games from database");

                // Redirect to action
                List<Game> games = new List<Game>();
                games.AddRange(gameslist);

                return Ok(games);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the GetAll Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // /api/game/gamebyid?id=1
        [HttpGet("GameByID")]
        public IActionResult GetSingleGame(int ID)
        {
            try
            {
                var game = _repo.GetAll.FirstOrDefault(x => x.Id == ID);

                if (game is null)
                {
                    _logger.LogError($"Game with id {ID}, hasn't been found!");
                    return BadRequest($"Game with id {ID}, hasn't been found!");
                }
                else
                {
                    _logger.LogInformation($"Returned game with {ID}");
                    // Redirect to action
                    return Ok(game);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the GetSingleGame Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public IActionResult Create(Game game)
        {
            try
            {
                if (game is null)
                {
                    _logger.LogError("Game object is null");
                    return BadRequest("Game is null");
                }

                _repo.Create(game);
                return CreatedAtRoute("GameByID", new { id = game.Id }, game);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Create Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        public IActionResult Update(int id, Game game)
        {
            try
            {
                if (game is null)
                {
                    _logger.LogError("Game object is null");
                    return BadRequest("Game is null");
                }

                var gameEntity = _repo.GetAll.FirstOrDefault(x => x.Id == id);
                if (gameEntity is null)
                {
                    _logger.LogError($"Game with id {id} not found");
                    return BadRequest($"Game with id {id} not found");
                }

                _repo.Update(id, game);

                return StatusCode(204, "Game updated successfully."); 
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Update Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var game = _repo.GetAll.FirstOrDefault(x => x.Id == id);
                if (game is null)
                {
                    _logger.LogError($"Game with id {id} not found");
                    return BadRequest($"Game with id {id} not found"); ;
                }

                _repo.Delete(id);

                return StatusCode(204, "Game removed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Delete Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
