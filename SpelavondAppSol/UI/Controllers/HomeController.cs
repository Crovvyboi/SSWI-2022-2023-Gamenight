using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UI.Models;

namespace UI.Controllers
{
    [Authorize(Policy = "EmailReq")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly IGamenightRepository _gamenightrepo;
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;

        public HomeController(ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signinManager,
            IGamenightRepository gamenightRepository,
            IUserRepository userRepository,
            IGameRepository gameRepository)
        {
            _userManager = userManager;
            _signInManager = signinManager;
            _logger = logger;

            _gamenightrepo = gamenightRepository;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
        }

        // overload index met userid, add user naar databasecontext bij registratie
        // Authorize bij alles behalve de index zonder parameter, login en register
        [AllowAnonymous]
        public IActionResult Index()
        {
            // Create viewmodel for HomeViewModel
            HomeViewModel model;

            // Get all gamenights, sort 
            ICollection<GameNight> gamenights = _gamenightrepo.GetAll.OrderBy(x => x.DateTime).ToList(); ;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;
                // Get name & gamenights of logged in user
                var user = _userRepository.GetSingleUser(email);
                if (user != null)
                {
                    model = new HomeViewModel(gamenights, user);

                    // pass list of gamenights & logged in user

                    return View(model);
                }
            }

            model = new HomeViewModel(gamenights);

            // Pass list of gamenights
            return View(model);
        }

        
        public IActionResult Gamenight(int gameNightid)
        {
            GameNight gameNight = _gamenightrepo.GetSingleGamenight(gameNightid);
            User user = _userRepository.GetSingleUser(User.Identity.Name);

            GamenightViewModel gamenightViewModel;
            if (user is not null)
            {
                gamenightViewModel = new GamenightViewModel(gameNight, user);
            }
            else
            {
                gamenightViewModel = new GamenightViewModel(gameNight);
            }


            if (gamenightViewModel != null)
            {
                return View(gamenightViewModel);
            }
            return Error();
        }

        public IActionResult EditGamenight(int gamenightid)
        {
            List<Game>? games = _gameRepository.GetAll as List<Game>;
            GameNight? gameNight = _gamenightrepo.GetSingleGamenight(gamenightid);
            User user = _userRepository.GetSingleUser(User.Identity.Name);

            EditGamenightViewModel editGamenightViewModel = new EditGamenightViewModel(games, gameNight, user);
            if (editGamenightViewModel != null)
            {
                return View(editGamenightViewModel);
            }
            return Error();
        }

        public IActionResult UserProfile(int userid)
        {
            User? user = _userRepository.GetAll.FirstOrDefault(x => x.Id == userid);
            ICollection<GameNight>? organized = _gamenightrepo.GetAllOrganized(userid).ToList();

            UserViewmodel model = new UserViewmodel(user, organized);

            if (user != null)
            {
                return View(model);
            }
            return Error();
        }

        public IActionResult CurrentUserProfile(string email)
        {
            User? user = _userRepository.GetSingleUser(email);


            if (user != null)
            {
                return RedirectToAction("UserProfile", "Home", new { userid = user.Id });
            }
            return Error();
        }

        public IActionResult CreateGameNight()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;
                // Get name & gamenights of logged in user
                var user = _userRepository.GetSingleUser(email);
                if (user != null)
                {
                    // Check if user is 18+
                    if (user.isEighteen())
                    {
                        // Pass user, games

                        ICollection<Game> games = _gameRepository.GetAll.ToList();

                        NewGamenightViewmodel newGamenightViewmodel = new NewGamenightViewmodel(games, user);

                        return View(newGamenightViewmodel);
                    }
                }
                ModelState.AddModelError("", "User not found");
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Not logged in");
            return RedirectToAction("Index");

        }

        public IActionResult Foodstuff(GameNight gameNightid)
        {
            var gamenight = _gamenightrepo.GetSingleGamenight(gameNightid.Id);
            if (gamenight != null)
            {
                FoodstuffViewmodel foodstuffViewmodel = new FoodstuffViewmodel(gamenight.Food.ToList(), gamenight);
                return View(foodstuffViewmodel);
            }

            ModelState.AddModelError("", "No gamenight found");
            return RedirectToAction("Index");
        }


        #region Login & Register
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Login funcitonality
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                // Sign in user
                var signinResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signinResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(string email, 
            string password, 
            string Firstname, 
            string Lastname,
            Gender gender,
            string street,
            int housenumber,
            string? housenumberaddition,
            string city,
            bool isvegan,
            bool isalcholic,
            bool hasnuts,
            DateTime birthday)
        {
            // Register funcitonality
            // Register new User for gamenights

            var user = new IdentityUser
            {
                UserName = email,
                Email = email
            };

            // Create user & insert in db
            User newuser = new User()
            {
                FirstName = Firstname,
                LastName = Lastname,
                Gender = gender,
                Street = street,
                HouseNumber = housenumber,
                HouseNumberAdditions = housenumberaddition,
                City = city,
                Birthday = birthday,
                Email = email,
                isVegan = isvegan,
                toleratesAlcohol = isalcholic,
                nutAlergy = hasnuts,
                Name = null
            };

            var result = await  _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                _userRepository.Create(newuser);
                User userforId = _userRepository.GetSingleUser(newuser.Email);

                // Sign in user
                await _userManager.AddClaimAsync(user, new Claim("EmailCheck", "email"));
                return RedirectToAction(nameof(Login), new {email, password});
            }


            _logger.LogInformation("Could not register user");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index"); 
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}