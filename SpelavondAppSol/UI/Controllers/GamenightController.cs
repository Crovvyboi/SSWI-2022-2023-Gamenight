using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/gamenight")]
    [Produces("application/json")]
    [ApiController]
    public class GamenightController : Controller
    {
        private ILogger<GamenightController> _logger;
        private IGamenightRepository _repo;
        private IUserRepository _userRepository;
        private IGameRepository _gameRepository;

        public GamenightController(ILogger<GamenightController> logger, IGamenightRepository repo, IUserRepository userRepository, IGameRepository gameRepository)
        {
            _logger = logger;
            _repo = repo;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var gamenights = _repo.GetAll;

                if (gamenights is null)
                {
                    return BadRequest("'GetAll returned null.");
                }
                _logger.LogInformation("Returned all gamenights from database");

                List<GameNight> gnlist = new List<GameNight>();
                gnlist.AddRange(gamenights);

                return Ok(gnlist);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the GetAll Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // /api/gamenight/gamenightbyid?id=1
        [HttpGet("GamenightByID")]
        public IActionResult GetSingleGame(int ID)
        {
            try
            {
                var gamenight = _repo.GetAll.FirstOrDefault(x => x.Id == ID);

                if (gamenight is null)
                {
                    _logger.LogError($"Game with id {ID}, hasn't been found!");
                    return BadRequest($"Game with id {ID}, hasn't been found!");
                }
                else
                {
                    _logger.LogInformation($"Returned game with {ID}");
                    // Redirect to action
                    return Ok(gamenight);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the GetSingleGame Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // /api/gamenight/gamenightbyorganizer?userid=1
        [HttpGet("GamenightByOrganizer")]
        public IActionResult GetGamesByOrganizer(int userid)
        {
            try
            {
                List<GameNight> gamenights = _repo.GetAll.Where(x => x.OrganizerID == userid).ToList();

                _logger.LogInformation("Returned all gamenights from database");
                if (gamenights.Count() == 0)
                {
                    _logger.LogError($"Game with organizerid {userid}, hasn't been found!");
                    return BadRequest($"Game with organizerid {userid}, hasn't been found!");
                }
                else
                {
                    _logger.LogInformation($"Returned games with organizerid {userid}");
                    // Redirect to action


                    List<GameNight> gnlist = new List<GameNight>();
                    gnlist.AddRange(gamenights);

                    return Ok(gnlist);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the GetAll Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("CreateGameNight")]
        public IActionResult Create([FromForm] string? name, [FromForm] string street, [FromForm] int housenumber, [FromForm] string? housenumberadd, [FromForm] string city, [FromForm] DateTime dateTime, [FromForm] int gameid, [FromForm] int maxplayers, [FromForm] bool ispotluck, [FromForm] bool iseighteenplus)
        {
            try
            {
                // Get user
                User? currentuser = _userRepository.GetSingleUser(User.Identity.Name);
                if (currentuser == null)
                {
                    _logger.LogError("user object is null");
                    return BadRequest("User is null");
                }

                // Check if inserted data is correct
                Game? game = _gameRepository.GetSingleGame(gameid);
                if (game == null)
                {
                    _logger.LogError("Game object is null");
                    return BadRequest("Game is null");
                }
                if (game.EighteenPlus)
                {
                    iseighteenplus = true;
                }

                // Construct Gamenight
                GameNight newGamenight = new GameNight()
                {
                    Organizer = currentuser,
                    Name = name,
                    Street = street,
                    HouseNumber = housenumber,
                    HouseNumberAdditions = housenumberadd,
                    City = city,
                    maxPlayers = maxplayers,
                    Players = new List<User>(),
                    DateTime = dateTime,
                    GameID = gameid,
                    isPotluck = ispotluck,
                    isEighteenPlus = iseighteenplus,
                    Food = new List<Foodstuffs>()
                };

                if (newGamenight is null)
                {
                    _logger.LogError("GameNight object is null");
                    return BadRequest("GameNight is null");
                }
                _repo.Create(newGamenight);

                return RedirectToAction("Foodstuff", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Create Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [AcceptVerbs("Post", "Put")]
        [Route("UpdateGamenight")]
        public IActionResult Update([FromForm] int gamenightid, [FromForm] string? name, [FromForm] string street, [FromForm] int housenumber, [FromForm] string? housenumberadd, [FromForm] string city, [FromForm] DateTime dateTime, [FromForm] int gameid, [FromForm] int maxplayers, [FromForm] bool ispotluck, [FromForm] bool iseighteenplus)
        {
            try
            {

                GameNight? gameEntity = _repo.GetSingleGamenight(gamenightid);
                if (gameEntity is null)
                {
                    _logger.LogError($"GameNight with id {gamenightid} not found");
                    return BadRequest($"GameNight with id {gamenightid} not found");
                }
                if (gameEntity.Organizer.Email != User.Identity.Name)
                {
                    _logger.LogError($"User does not own gamenight");
                    return BadRequest($"User does not own gamenight");
                }

                Game? game = _gameRepository.GetSingleGame(gameid);
                if (game is null)
                {
                    _logger.LogError($"Game with id {gameid} not found");
                    return BadRequest($"Game with id {gameid} not found");
                }

                // Check if gamenight can handle playersize & potluck change
                if (gameEntity.Players.Count() > maxplayers)
                {
                    _logger.LogError($"Max player size is too small (You already have people who applied)");
                    return BadRequest($"Max player size is too small (You already have people who applied)");
                }
                if (gameEntity.Food.Count() > 1 && !ispotluck)
                {
                    _logger.LogError($"Cannot cancel the potluck (You already have people who applied)");
                    return BadRequest($"Cannot cancel the potluck (You already have people who applied)");
                }

                //Construct new gamenight
                GameNight gamenight = new GameNight()
                {
                    Name = name,
                    Street = street,
                    HouseNumber = housenumber,
                    HouseNumberAdditions = housenumberadd,
                    City = city,
                    isEighteenPlus = iseighteenplus,
                    isPotluck = ispotluck,
                    DateTime = dateTime,
                    GameID = gameid,
                    PlayedGame = game,
                    maxPlayers = maxplayers
                };

                gameEntity.UpdateFromForm(gamenight);

                //Update at id

                _repo.Update(gamenightid, gameEntity);

                return RedirectToAction("Gamenight", "Home", new { gameNightid = gamenightid});
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Update Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [AcceptVerbs("Get", "Delete")]
        [Route("RemoveGamenight")]
        public IActionResult Delete(int id)
        {
            try
            {
                var gamenight = _repo.GetSingleGamenight(id);
                if (gamenight is null)
                {
                    _logger.LogError($"GameNight with id {id} not found");
                    return BadRequest($"GameNight with id {id} not found");
                }
                if (gamenight.Players.Count() != 0)
                {
                    _logger.LogError("Players are already present at gamenight. Gamenights can only be canceled without players.");
                    return BadRequest("Players are already present at gamenight. Gamenights can only be canceled without players.");
                }
                if (gamenight.Organizer.Email != User.Identity.Name)
                {
                    _logger.LogError($"User does not own gamenight");
                    return BadRequest($"User does not own gamenight");
                }

                _repo.Delete(id);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Delete Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // api/gamenight/addplayer?playerid=1&gamenightid=1
        [AcceptVerbs("Post", "Put")]
        [Route("AddPlayer")]
        public IActionResult AddPlayer([FromForm] int gamenightid, [FromForm] string? Name, [FromForm] string? Description, [FromForm] bool isvegan, [FromForm] bool isalcholic, [FromForm] bool hasnuts)
        {
            try
            {
                

                var player = _userRepository.GetSingleUser(User.Identity.Name);
                if (player is null)
                {
                    _logger.LogError($"User with email {User.Identity.Name} not found");
                    return BadRequest($"User with email {User.Identity.Name} not found");
                }

                var gameEntity = _repo.GetSingleGamenight(gamenightid);
                if (gameEntity is null)
                {
                    _logger.LogError($"GameNight with id {gamenightid} not found");
                    return BadRequest($"GameNight with id {gamenightid} not found");
                }

                if (player.playerat.Count() != 0)
                {
                    if (player.playerat.Any(x => x.DateTime.Date == gameEntity.DateTime.Date))
                    {
                        _logger.LogError($"Player already subscribed to gamenight on {gameEntity.DateTime.Date}");
                        return BadRequest($"Player already subscribed to gamenight on {gameEntity.DateTime.Date}");
                    }
                }

                if (gameEntity.isEighteenPlus || gameEntity.PlayedGame.EighteenPlus)
                {
                    if (!player.isEighteen())
                    {
                        _logger.LogError("Players under 18 cannot apply for 18+ gamenights.");
                        return BadRequest("Players under 18 cannot apply for 18+ gamenights.");
                    }
                }

                _repo.AddPlayerToGameNight(player, gamenightid);

                if (gameEntity.isPotluck)
                {

                    Foodstuffs foodstuffs = new Foodstuffs()
                    {
                        Name = Name,
                        Description = Description,
                        userid = player.Id,
                        isVegan = isvegan,
                        isAlcoholic = isalcholic,
                        nutAlergy = hasnuts
                    };


                    _repo.AddFoodstuffToGameNight(foodstuffs, gameEntity);
                }
  
                return RedirectToAction("Gamenight", "Home", new { gameNightid = gamenightid });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Add Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // api/gamenight/removeplayer?playerid=1&gamenightid=1
        [AcceptVerbs("Get", "Delete")]
        [Route("RemovePlayer")]
        public IActionResult RemovePlayer(int gamenightid)
        {
            try
            {
                var player = _userRepository.GetSingleUser(User.Identity.Name);
                if (player is null)
                {
                    _logger.LogError($"Player not found");
                    return BadRequest("Player not found");
                }

                var gameEntity = _repo.GetSingleGamenight(gamenightid);
                if (gameEntity is null)
                {
                    _logger.LogError($"GameNight with id {gamenightid} not found");
                    return BadRequest($"GameNight with id {gamenightid} not found");
                }

                if (gameEntity.Players.FirstOrDefault(x => x.Email == player.Email) is null)
                {
                    _logger.LogError($"User not present in gamenight");
                    return BadRequest($"User not present in gamenight");
                }

                _repo.RemovePlayerFromGameNight(player.Id, gameEntity);

                return RedirectToAction("Gamenight", "Home", new { gameNightid = gamenightid });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Remove Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #region Foodstuff
        // Add, update & remove food

        // /api/gamenight/addfood
        [HttpPost("AddFood")]
        public IActionResult AddFoodToGamenight([FromForm] int gamenightid, [FromForm] string Name, [FromForm] string Description, [FromForm] bool isvegan, [FromForm] bool isalcholic, [FromForm] bool hasnuts)
        {
            try
            {
                // get gamenight
                var gamenight = _repo.GetSingleGamenight(gamenightid);
                if (gamenight is null)
                {
                    _logger.LogError($"GameNight with id {gamenightid} not found");
                    return BadRequest($"GameNight with id {gamenightid} not found");
                }

                User? thisuser = _userRepository.GetSingleUser(User.Identity.Name);
                if (thisuser is null)
                {
                    _logger.LogError($"User with id {User.Identity.Name} not found");
                    return BadRequest($"User with id {User.Identity.Name} not found");
                }
                if (gamenight.Food.Any(x => x.userid == thisuser.Id) == true)
                {
                    _logger.LogError($"User with id {User.Identity.Name} already has food item in potluck. Update instead.");
                    return BadRequest($"User with id {User.Identity.Name} already has food item in potluck. Update instead.");
                }

                Foodstuffs foodstuffs = new Foodstuffs()
                {
                    Name = Name,
                    Description = Description,
                    userid = thisuser.Id,
                    isVegan = isvegan,
                    isAlcoholic = isalcholic,
                    nutAlergy = hasnuts
                };

                _repo.AddFoodstuffToGameNight(foodstuffs, gamenight);
                
                return RedirectToAction("Foodstuff", "Home", gamenight);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Add Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [AcceptVerbs("Post", "Put")]
        [Route("updatefood")]
        public IActionResult UpdateFoodToGamenight([FromForm] int gamenightid, [FromForm] int foodstuffid, [FromForm] string Name, [FromForm] string Description, [FromForm] bool isvegan, [FromForm] bool isalcholic, [FromForm] bool hasnuts)
        {
            try
            {

                // get gamenight
                var gamenight = _repo.GetSingleGamenight(gamenightid);
                if (gamenight is null)
                {
                    _logger.LogError($"GameNight with id {gamenightid} not found");
                    return BadRequest($"GameNight with id {gamenightid} not found");
                }

                var currentuser = _userRepository.GetSingleUser(User.Identity.Name);
                if (currentuser is null)
                {
                    _logger.LogError($"User with email {User.Identity.Name} not found");
                    return BadRequest($"User with email {User.Identity.Name} not found");
                }
                if (gamenight.Food.First(x => x.userid == currentuser.Id).id != foodstuffid)
                {
                    _logger.LogError($"User does not own food item!");
                    return BadRequest($"User does not own food item!");
                }

                Foodstuffs newfoodstuffs = new Foodstuffs()
                {
                    Name = Name,
                    Description = Description,
                    userid = currentuser.Id,
                    isVegan = isvegan,
                    isAlcoholic = isalcholic,
                    nutAlergy = hasnuts
                };

                _repo.UpdateFoodstuffInGamenight(foodstuffid, newfoodstuffs, gamenight);

                return RedirectToAction("Foodstuff", "Home", gamenight);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Update Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [AcceptVerbs("Get", "Delete")]
        [Route("RemoveFood")]
        public IActionResult RemoveFoodFromGamenight(int foodstuffid, int gamenightid)
        {
            try
            {
                // get gamenight
                var gamenight = _repo.GetSingleGamenight(gamenightid);
                if (gamenight is null)
                {
                    _logger.LogError($"GameNight with id {gamenightid} not found");
                    return BadRequest($"GameNight with id {gamenightid} not found");
                }
                var currentuser = _userRepository.GetSingleUser(User.Identity.Name);
                if (currentuser is null)
                {
                    _logger.LogError($"User with email {User.Identity.Name} not found");
                    return BadRequest($"User with email {User.Identity.Name} not found");
                }
                if (gamenight.Food.First(x => x.userid == currentuser.Id).id != foodstuffid)
                {
                    _logger.LogError($"User does not own food item!");
                    return BadRequest($"User does not own food item!");
                }

                _repo.RemoveFoodstuffFromGameNight(foodstuffid, gamenight);

                return RedirectToAction("Foodstuff", "Home", gamenight);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Remove Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion


    }
}
