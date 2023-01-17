using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Seeder
    {
        private DatabaseContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private ILogger<DatabaseContext> _logger;

        public Seeder(DatabaseContext context, UserManager<IdentityUser> userManager, ILogger<DatabaseContext> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task EnsurePopulated(bool dropExisting = false)
        {
            if (dropExisting)
            {
                _context.Database.EnsureDeleted();
            }

            _context.Database.Migrate();

            _context.SaveChanges();

            #region Game
            if (_context.Game?.Count() == 0)
            {
                _logger.LogInformation("Preparing to seed Game");
                _context.Game.AddRange(new[]
                {
                    new Game()
                    {
                        Name = "Ticket to Ride",
                        Description = "Become the biggest train baron and build your railways all across America",
                        EighteenPlus = false,
                        GameType = Domain.Models.Type.Board,
                        Genre = Genre.Strategy,
                        PicturePath = " "
                    },
                    new Game()
                    {
                        Name = "Ticket to Ride Europe",
                        Description = "Become the biggest train baron and build your railways all across Europe",
                        EighteenPlus = false,
                        GameType = Domain.Models.Type.Board,
                        Genre = Genre.Strategy,
                        PicturePath = " "
                    },
                    new Game()
                    {
                        Name = "Dnd 5e: Curse of Strahd",
                        Description = "A spooky campaign for 5th edition Dungeons & Dragons",
                        EighteenPlus = true,
                        GameType = Domain.Models.Type.Long_term,
                        Genre = Genre.TTRPG,
                        PicturePath = " "
                    },
                    new Game()
                    {
                        Name = "Mountains of Madness",
                        Description = "Gather evidence of eldritch presence in the artic, before buckling under the maddening influences of the expedition.",
                        EighteenPlus = false,
                        GameType = Domain.Models.Type.Board,
                        Genre = Genre.Coöperation,
                        PicturePath = " "
                    },                    
                    new Game()
                    {
                        Name = "Betrayal at the House on the Hill",
                        Description = "Explore an old gothic mansion, before one of you betrays the rest.",
                        EighteenPlus = false,
                        GameType = Domain.Models.Type.Board,
                        Genre = Genre.Coöperation,
                        PicturePath = " "
                    },                    
                    new Game()
                    {
                        Name = "Loveletter",
                        Description = "Ooh la la. The princess has a few secret adimirers. Win her heart in this strategic card game.",
                        EighteenPlus = false,
                        GameType = Domain.Models.Type.Card,
                        Genre = Genre.Strategy,
                        PicturePath = " "
                    },                    
                    new Game()
                    {
                        Name = "Coup",
                        Description = "Who will betray, who will work together, who will be the last one standing?",
                        EighteenPlus = false,
                        GameType = Domain.Models.Type.Card,
                        Genre = Genre.Social_deduction,
                        PicturePath = " "
                    },                    
                    new Game()
                    {
                        Name = "Cards against humanity",
                        Description = "Score the most points by being the most vulgar, clever or pop culture aware of them all in this modern classic.",
                        EighteenPlus = true,
                        GameType = Domain.Models.Type.Card,
                        Genre = Genre.Party,
                        PicturePath = " "
                    },
                    new Game()
                    {
                        Name = "Dnd 5e: Tomb of Horrors",
                        Description = "Find the treasures of the ancient demilich in this legacy Dungeons & Dragons dungeon.",
                        EighteenPlus = false,
                        GameType = Domain.Models.Type.Long_term,
                        Genre = Genre.TTRPG,
                        PicturePath = " "
                    },

                });
                _context.SaveChanges();

                _logger.LogInformation("Game seeded");
            }
            else
            {
                _logger.LogInformation("Game not seeded");
            }
            #endregion

            #region User
            if (_context.Users?.Count() == 0)
            {
                _logger.LogInformation("Preparing to seed Users");
                _context.Users.AddRange(new[]
                {
                    new User() {
                        FirstName = "Jane",
                        LastName = "Doe",
                        Email = "Jane.D@email.com",
                        City = "Magnol",
                        HouseNumber = 17,
                        Street = "Danishlane",
                        Birthday = new DateTime(1996, 5, 24),
                        Gender = Gender.Woman,
                        Name = null,
                        HouseNumberAdditions = null,
                        isVegan = false,
                        nutAlergy = false,
                        toleratesAlcohol = true

                    },
                    new User(){
                        FirstName = "Jack",
                        LastName = "Doe",
                        Email = "Jack.D@email.com",
                        City = "Magnol", 
                        HouseNumber = 17, 
                        Street = "Danishlane" ,
                        Birthday = new DateTime(2000, 12, 12),
                        Gender = Gender.Man,
                        Name = null,
                        HouseNumberAdditions = null,
                        isVegan = false,
                        nutAlergy = true,
                        toleratesAlcohol = true
                    },
                    new User(){
                        FirstName = "John",
                        LastName = "Mann",
                        Email = "MannMan@email.com",
                        City = "St. Huge",
                        HouseNumber = 59,
                        Street = "Don plaza" ,
                        Birthday = new DateTime(1999, 11, 25),
                        Gender = Gender.Man,
                        Name = null,
                        HouseNumberAdditions = "a",
                        isVegan = false,
                        nutAlergy = false,
                        toleratesAlcohol = true
                    },
                    new User(){
                        FirstName = "Maria",
                        LastName = "Illsum",
                        Email = "ImIllM@email.com",
                        City = "St. Huge",
                        HouseNumber = 59,
                        Street = "Don plaza" ,
                        Birthday = new DateTime(1996, 1, 2),
                        Gender = Gender.NonBinary,
                        Name = null,
                        HouseNumberAdditions = "b",
                        isVegan = false,
                        nutAlergy = false,
                        toleratesAlcohol = true
                    },
                    new User(){
                        FirstName = "Isa",
                        LastName = "Likable",
                        Email = "LikeImIsa@email.com",
                        City = "St. Huge",
                        HouseNumber = 2,
                        Street = "Agariastreet" ,
                        Birthday = new DateTime(2005, 8, 14),
                        Gender = Gender.Woman,
                        Name = null,
                        HouseNumberAdditions = null,
                        isVegan = false,
                        nutAlergy = false,
                        toleratesAlcohol = false
                    },
                    new User(){
                        FirstName = "Cien",
                        LastName = "of Irish",
                        Email = "GaelicTrash@email.com",
                        City = "Greenfield",
                        HouseNumber = 202,
                        Street = "Cloverlane" ,
                        Birthday = new DateTime(1989, 6, 10),
                        Gender = Gender.Other,
                        Name = null,
                        HouseNumberAdditions = "b",
                        isVegan = false,
                        nutAlergy = false,
                        toleratesAlcohol = true
                    }

                });
                _context.SaveChanges();
                _logger.LogInformation("Users seeded");
            }
            else
            {
                _logger.LogInformation("Users not seeded");
            }
            #endregion

            #region GameNight
            if (_context.GameNight?.Count() == 0)
            {
                _logger.LogInformation("Preparing to seed GameNight");

                _context.GameNight.AddRange(
                    new GameNight()
                    {
                        OrganizerID = 1,
                        GameID = 2,
                        DateTime = DateTime.Now,
                        Food = new List<Foodstuffs>()
                    {
                        new Foodstuffs()
                        {
                            Name = "Train cookies",
                            Description = "Thematic",
                            isVegan = true,
                            isAlcoholic = false,
                            nutAlergy = false,
                            userid = 1,

                        }
                    },
                        isPotluck = true,
                        isEighteenPlus = false,
                        City = "Magnol",
                        HouseNumber = 17,
                        Street = "Danishlane",
                        maxPlayers = 2,
                        Players = new List<User>
                        {

                        }
                    },
                    new GameNight()
                    {
                        OrganizerID = 4,
                        GameID = 3,
                        DateTime = new DateTime(2023, 4, 23, 17, 0, 0),
                        Food = new List<Foodstuffs>()
                    {
                        new Foodstuffs()
                        {
                            Name = "\'Pls no fireball\' cake",
                            Description = "Good, spicy cake",
                            isVegan = false,
                            isAlcoholic = false,
                            nutAlergy = true,
                            userid = 1,

                        }
                    },
                        isPotluck = false,
                        isEighteenPlus = false,
                        Name = "Gaming club \'The turnt die\'",
                        City = "St. Huge",
                        HouseNumber = 59,
                        Street = "Don Plaza",
                        HouseNumberAdditions = "b",
                        maxPlayers = 6,
                        Players = new List<User>
                        {

                        }
                    },
                    new GameNight()
                    {
                        OrganizerID = 2,
                        GameID = 6,
                        DateTime = new DateTime(2023, 1, 29, 19, 30, 0),
                        Food = new List<Foodstuffs>()
                    {
                        new Foodstuffs()
                        {
                            Name = "Popcorn",
                            Description = "Salty",
                            isVegan = true,
                            isAlcoholic = false,
                            nutAlergy = false,
                            userid = 6,

                        },
                        new Foodstuffs()
                        {
                            Name = "Apples",
                            Description = "Nice and healthy",
                            isVegan = true,
                            isAlcoholic = false,
                            nutAlergy = false,
                            userid = 3,

                        },                        
                        new Foodstuffs()
                        {
                            Name = "Blackberry punch",
                            Description = ":)",
                            isVegan = true,
                            isAlcoholic = true,
                            nutAlergy = false,
                            userid = 4,

                        }
                    },
                        isPotluck = true,
                        isEighteenPlus = false,
                        City = "Magnol",
                        HouseNumber = 17,
                        Street = "Danishlane",
                        maxPlayers = 3,
                        Players = new List<User>
                        {

                        }
                    },
                    new GameNight()
                    {
                        OrganizerID = 3,
                        GameID = 8,
                        DateTime = new DateTime(2023, 2, 13, 20, 30, 0),
                        Food = new List<Foodstuffs>()
                    {
                        new Foodstuffs()
                        {
                            Name = "Thunderbolt taffee",
                            Description = "You know \'em, you love \'em",
                            isVegan = false,
                            isAlcoholic = false,
                            nutAlergy = false,
                            userid = 3,

                        }
                    },
                        isPotluck = true,
                        isEighteenPlus = true,
                        City = "St. Huge",
                        HouseNumber = 59,
                        Street = "Don Plaza",
                        HouseNumberAdditions = "a",
                        maxPlayers = 4,
                        Players = new List<User>
                        {

                        }
                    },
                    new GameNight()
                    {
                        OrganizerID = 5,
                        GameID = 9,
                        DateTime = new DateTime(2023, 2, 13, 22, 30, 0),
                        Food = new List<Foodstuffs>()
                    {
                        new Foodstuffs()
                        {
                            Name = "Thunderbolt taffee",
                            Description = "You know \'em, you love \'em",
                            isVegan = false,
                            isAlcoholic = false,
                            nutAlergy = true,
                            userid = 5,

                        }
                    },
                        isPotluck = true,
                        isEighteenPlus = true,
                        City = "St. Huge",
                        HouseNumber = 59,
                        Street = "Don Plaza",
                        HouseNumberAdditions = "a",
                        maxPlayers = 4,
                        Players = new List<User>
                        {

                        }
                    },
                    new GameNight()
                    {
                        OrganizerID = 1,
                        GameID = 5,
                        DateTime = new DateTime(2024, 2, 13, 22, 30, 0),
                        Food = new List<Foodstuffs>()
                    {                              
                        new Foodstuffs()
                        {
                            Name = "Thunderbolt taffee",
                            Description = "You know \'em, you love \'em",
                            isVegan = false,
                            isAlcoholic = false,
                            nutAlergy = true,
                            userid = 1,

                        }
                    },
                        isPotluck = false,
                        isEighteenPlus = true,
                        City = "St. Huge",
                        HouseNumber = 59,
                        Street = "Don Plaza",
                        HouseNumberAdditions = "a",
                        maxPlayers = 4,
                        Players = new List<User>
                        {

                        }
                    }

                );
                _context.SaveChanges();
                _logger.LogInformation("GameNight seeded");
            }
            else
            {
                _logger.LogInformation("GameNight not seeded");
            }
            #endregion

            #region PlayerGamenight
            if (_context.userGamenights?.Count() == 0)
            {
                _logger.LogInformation("Preparing to seed Game");
                _context.userGamenights.AddRange(new[]
                {
                    new PlayerGamenight()
                    {
                        gameNightID = 1,
                        playerID = 3,
                    },
                    new PlayerGamenight()
                    {
                        gameNightID = 1,
                        playerID = 5
                    },
                    new PlayerGamenight()
                    {
                        gameNightID = 2,
                        playerID = 1
                    },
                    new PlayerGamenight()
                    {
                        gameNightID = 2,
                        playerID = 6
                    },                    
                    new PlayerGamenight()
                    {
                        gameNightID = 3,
                        playerID = 6
                    },
                    new PlayerGamenight()
                    {
                        gameNightID = 3,
                        playerID = 3
                    },
                    new PlayerGamenight()
                    {
                        gameNightID = 3,
                        playerID = 4
                    },

                });
                _context.SaveChanges();

                _logger.LogInformation("Game seeded");
            }
            #endregion


            #region Security seeding
            const string USERNAME = "Jack.D@email.com";
            const string PASSWORD = "Secret123";

            const string USERNAME2 = "Jane.D@email.com";
            const string PASSWORD2 = "Jack5Trash";

            const string USERNAME3 = "MannMan@email.com";
            const string PASSWORD3 = "Hamboi2";

            const string USERNAME4 = "ImIllM@email.com";
            const string PASSWORD4 = "SickAs445";

            const string USERNAME5 = "LikeImIsa@email.com";
            const string PASSWORD5 = "PartyLikeIts99";

            const string USERNAME6 = "GaelicTrash@email.com";
            const string PASSWORD6 = "6LuckyCharm9";


            if (dropExisting)
            {
                var exisitinguser = await _userManager.FindByNameAsync(USERNAME); 
                if (exisitinguser != null)
                {
                    await _userManager.DeleteAsync(exisitinguser);
                }
                var exisitinguser2 = await _userManager.FindByNameAsync(USERNAME2);
                if (exisitinguser2 != null)
                {
                    await _userManager.DeleteAsync(exisitinguser2);
                }
                var exisitinguser3 = await _userManager.FindByNameAsync(USERNAME3);
                if (exisitinguser3 != null)
                {
                    await _userManager.DeleteAsync(exisitinguser3);
                }
                var exisitinguser4 = await _userManager.FindByNameAsync(USERNAME4);
                if (exisitinguser4 != null)
                {
                    await _userManager.DeleteAsync(exisitinguser4);
                }
                var exisitinguser5 = await _userManager.FindByNameAsync(USERNAME5);
                if (exisitinguser5 != null)
                {
                    await _userManager.DeleteAsync(exisitinguser5);
                }
                var exisitinguser6 = await _userManager.FindByNameAsync(USERNAME6);
                if (exisitinguser6 != null)
                {
                    await _userManager.DeleteAsync(exisitinguser6);
                }
            }

            IdentityUser user1 = await _userManager.FindByNameAsync(USERNAME);
            if (user1 == null)
            {
                user1 = new IdentityUser()
                {
                    UserName = USERNAME,
                    Email = USERNAME
                };
                await _userManager.CreateAsync(user1, PASSWORD);
                await _userManager.AddClaimAsync(user1, new Claim("EmailCheck", "email"));
            }
            IdentityUser user2 = await _userManager.FindByNameAsync(USERNAME2);
            if (user2 == null)
            {
                user2 = new IdentityUser()
                {
                    UserName = USERNAME2,
                    Email = USERNAME2
                };
                await _userManager.CreateAsync(user2, PASSWORD2);
                await _userManager.AddClaimAsync(user2, new Claim("EmailCheck", "email"));
            }
            IdentityUser user3 = await _userManager.FindByNameAsync(USERNAME3);
            if (user3 == null)
            {
                user3 = new IdentityUser()
                {
                    UserName = USERNAME3,
                    Email = USERNAME3
                };
                await _userManager.CreateAsync(user3, PASSWORD3);
                await _userManager.AddClaimAsync(user3, new Claim("EmailCheck", "email"));
            }
            IdentityUser user4 = await _userManager.FindByNameAsync(USERNAME4);
            if (user4 == null)
            {
                user4 = new IdentityUser()
                {
                    UserName = USERNAME4,
                    Email = USERNAME4
                };
                await _userManager.CreateAsync(user4, PASSWORD4);
                await _userManager.AddClaimAsync(user4, new Claim("EmailCheck", "email"));
            }
            IdentityUser user5 = await _userManager.FindByNameAsync(USERNAME5);
            if (user5 == null)
            {
                user5 = new IdentityUser()
                {
                    UserName = USERNAME5,
                    Email = USERNAME5
                };
                await _userManager.CreateAsync(user5, PASSWORD5);
                await _userManager.AddClaimAsync(user5, new Claim("EmailCheck", "email"));
            }
            IdentityUser user6 = await _userManager.FindByNameAsync(USERNAME6);
            if (user6 == null)
            {
                user6 = new IdentityUser()
                {
                    UserName = USERNAME6,
                    Email = USERNAME6
                };
                await _userManager.CreateAsync(user6, PASSWORD6);
                await _userManager.AddClaimAsync(user6, new Claim("EmailCheck", "email"));
            }
            #endregion

        }
    }
}
