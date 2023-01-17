using AutoMapper;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/user")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILogger<UserController> _logger;
        private IUserRepository _repo;

        public UserController(ILogger<UserController> logger, IUserRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _repo.GetAll;

                if (users is null)
                {
                    return BadRequest("'GetAll returned null.");
                }
                _logger.LogInformation("Returned all users from database");


                List<User> usersList = new List<User>();
                usersList.AddRange(users);
 
                return Ok(usersList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the GetAll Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // /api/game/userbyid?email=x
        [HttpGet("UserByEmail")]
        public IActionResult GetSingleUser(string Email)
        {
            try
            {
                User user = _repo.GetAll.FirstOrDefault(x => x.Email == Email);

                if (user is null)
                {
                    _logger.LogError($"User with email = {Email}, hasn't been found!");
                    return BadRequest($"User with email = {Email}, hasn't been found!");
                }
                else
                {
                    _logger.LogInformation($"Returned user with {Email}");
                    // Redirect to action
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the GetSingleUser Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public IActionResult Create(User? user)
        {
            try
            {
                if (user is null)
                {
                    _logger.LogError("User object is null");
                    return BadRequest("User is null");
                }
                if (_repo.GetAll.FirstOrDefault(x => x.Email == user.Email) != null )
                {
                    _logger.LogError("User already exists");
                    return BadRequest("User already exists");
                }
                _repo.Create(user);
                return CreatedAtRoute("UserByID", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Create Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        public IActionResult Update(User? user)
        {
            try
            {
                if (user is null)
                {
                    _logger.LogError("User object is null");
                    return BadRequest("User is null");
                }

                var all = _repo.GetAll;
                var userEntity = all?.FirstOrDefault(x => x.Id == user.Id);
                if (userEntity is null)
                {
                    _logger.LogError($"User with id {user.Id} not found");
                    return BadRequest($"User with id {user.Id} not found");
                }
                if (userEntity.Email != User.Identity.Name)
                {
                    _logger.LogError("Invalid owner of object");
                    return BadRequest("Invalid owner of object");
                }

                _repo.Update(user.Id, user);

                return StatusCode(204, "User updated successfully.");
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
                var all = _repo.GetAll;
                var user = all?.FirstOrDefault(x => x.Id == id);

                if (user is null)
                {
                    _logger.LogError($"User with id {id} not found");
                    return BadRequest($"User with id {id} not found");
                }
                if (user.Email != User.Identity.Name)
                {
                    _logger.LogError("Invalid owner of object");
                    return BadRequest("Invalid owner of object");
                }

                _repo.Delete(id);

                return StatusCode(204, "User removed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Delete Action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
