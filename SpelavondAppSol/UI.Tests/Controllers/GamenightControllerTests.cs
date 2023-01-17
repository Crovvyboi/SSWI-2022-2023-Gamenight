using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UI.Controllers;
using Type = Domain.Models.Type;

namespace UI.Tests.Controllers
{
    public class GamenightControllerTests
    {
        private LoggerFactory loggerFactory = new LoggerFactory();
        [Fact]
        public void Can_Use_Repo()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);

            // Act
            IEnumerable<GameNight>? result = (sut.GetAll() as OkObjectResult)?.Value as List<GameNight>;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);

        }

        #region Get Requests

        [Fact]
        public void Gamenight_GetAll()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);

            // Act
            IEnumerable<GameNight>? result = (sut.GetAll() as OkObjectResult)?.Value as List<GameNight>;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Gamenight_GetSingle()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);

            // Act
            GameNight? result = (sut.GetSingleGame(1) as OkObjectResult)?.Value as GameNight;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Gamenight_GetSingle_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);

            // Act
            ObjectResult? result = sut.GetSingleGame(500) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Gamenight_GetByOrganizer()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);

            // Act
            IEnumerable<GameNight>? result = (sut.GetGamesByOrganizer(1) as OkObjectResult)?.Value as List<GameNight>;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Gamenight_GetByOrganizer_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);

            // Act
            ObjectResult? result = sut.GetGamesByOrganizer(500) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        #endregion

        #region Create Requests
        [Fact]
        public void Gamenight_Create()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("a@a.nl")).Returns(GetSingleUser("a@a.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            string name = "Name";
            string street = "Street"; 
            int housenumber = 5;
            string housenumberadd = ""; 
            string city = "City";
            DateTime dateTime = DateTime.Now;
            int gameid = 2;
            int maxplayers = 4;
            bool ispotluck = false; 
            bool iseighteenplus = false;

            // Act
            RedirectToActionResult? result = sut.Create(name,street,housenumber,housenumberadd,city,dateTime,gameid,maxplayers,ispotluck,iseighteenplus) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Foodstuff", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void Gamenight_Create_Game_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("a@a.nl")).Returns(GetSingleUser("a@a.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(200)).Returns(GetSingleGame(200));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            string name = "Name";
            string street = "Street";
            int housenumber = 5;
            string housenumberadd = "";
            string city = "City";
            DateTime dateTime = DateTime.Now;
            int gameid = 2;
            int maxplayers = 4;
            bool ispotluck = false;
            bool iseighteenplus = false;

            // Act
            ObjectResult? result = sut.Create(name, street, housenumber, housenumberadd, city, dateTime, gameid, maxplayers, ispotluck, iseighteenplus) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Gamenight_Create_User_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("a2@a.nl")).Returns(GetSingleUser("a2@a.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            string name = "Name";
            string street = "Street";
            int housenumber = 5;
            string housenumberadd = "";
            string city = "City";
            DateTime dateTime = DateTime.Now;
            int gameid = 2;
            int maxplayers = 4;
            bool ispotluck = false;
            bool iseighteenplus = false;

            // Act
            ObjectResult? result = sut.Create(name, street, housenumber, housenumberadd, city, dateTime, gameid, maxplayers, ispotluck, iseighteenplus) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
        #endregion

        #region Update Requests
        [Fact]
        public void Gamenight_Update()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("a@a.nl")).Returns(GetSingleUser("a@a.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;
            string name = "Name";
            string street = "Street";
            int housenumber = 5;
            string housenumberadd = "";
            string city = "City";
            DateTime dateTime = DateTime.Now;
            int gameid = 2;
            int maxplayers = 6;
            bool ispotluck = true;
            bool iseighteenplus = false;

            // Act
            RedirectToActionResult? result = sut.Update(gamenightid, name, street, housenumber, housenumberadd, city, dateTime, gameid, maxplayers, ispotluck, iseighteenplus) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Gamenight", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void Gamenight_Update_Gamenight_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(20)).Returns(GetSingleGamenight(20));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("a@a.nl")).Returns(GetSingleUser("a@a.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 20;
            string name = "Name";
            string street = "Street";
            int housenumber = 5;
            string housenumberadd = "";
            string city = "City";
            DateTime dateTime = DateTime.Now;
            int gameid = 2;
            int maxplayers = 6;
            bool ispotluck = true;
            bool iseighteenplus = false;

            // Act
            ObjectResult? result = sut.Update(gamenightid, name, street, housenumber, housenumberadd, city, dateTime, gameid, maxplayers, ispotluck, iseighteenplus) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("GameNight with id 20 not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_Update_User_Does_Not_Own_Gamenight()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "b@b.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("b@b.nl")).Returns(GetSingleUser("b@b.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;
            string name = "Name";
            string street = "Street";
            int housenumber = 5;
            string housenumberadd = "";
            string city = "City";
            DateTime dateTime = DateTime.Now;
            int gameid = 2;
            int maxplayers = 6;
            bool ispotluck = true;
            bool iseighteenplus = false;

            // Act
            ObjectResult? result = sut.Update(gamenightid, name, street, housenumber, housenumberadd, city, dateTime, gameid, maxplayers, ispotluck, iseighteenplus) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User does not own gamenight", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_Update_Game_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("a@a.nl")).Returns(GetSingleUser("a@a.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(40)).Returns(GetSingleGame(40));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;
            string name = "Name";
            string street = "Street";
            int housenumber = 5;
            string housenumberadd = "";
            string city = "City";
            DateTime dateTime = DateTime.Now;
            int gameid = 40;
            int maxplayers = 6;
            bool ispotluck = true;
            bool iseighteenplus = false;

            // Act
            ObjectResult? result = sut.Update(gamenightid, name, street, housenumber, housenumberadd, city, dateTime, gameid, maxplayers, ispotluck, iseighteenplus) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Game with id 40 not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_Update_New_Maxplayer_Too_Few()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("a@a.nl")).Returns(GetSingleUser("a@a.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(1)).Returns(GetSingleGame(1));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;
            string name = "Name";
            string street = "Street";
            int housenumber = 5;
            string housenumberadd = "";
            string city = "City";
            DateTime dateTime = DateTime.Now;
            int gameid = 1;
            int maxplayers = 2;
            bool ispotluck = true;
            bool iseighteenplus = false;

            // Act
            ObjectResult? result = sut.Update(gamenightid, name, street, housenumber, housenumberadd, city, dateTime, gameid, maxplayers, ispotluck, iseighteenplus) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Max player size is too small (You already have people who applied)", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_Update_Cannot_Revoke_Potluck()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("a@a.nl")).Returns(GetSingleUser("a@a.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(1)).Returns(GetSingleGame(1));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;
            string name = "Name";
            string street = "Street";
            int housenumber = 5;
            string housenumberadd = "";
            string city = "City";
            DateTime dateTime = DateTime.Now;
            int gameid = 1;
            int maxplayers = 6;
            bool ispotluck = false;
            bool iseighteenplus = false;

            // Act
            ObjectResult? result = sut.Update(gamenightid, name, street, housenumber, housenumberadd, city, dateTime, gameid, maxplayers, ispotluck, iseighteenplus) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Cannot cancel the potluck (You already have people who applied)", result.Value.ToString());
        }

        #endregion

        #region Remove Requests
        [Fact]
        public void Gamenight_Remove()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(2)).Returns(GetSingleGamenight(2));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("a@a.nl")).Returns(GetSingleUser("a@a.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 2;

            // Act
            RedirectToActionResult? result = sut.Delete(gamenightid) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void Gamenight_Remove_Gamenight_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(40)).Returns(GetSingleGamenight(40));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("a@a.nl")).Returns(GetSingleUser("a@a.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 40;

            // Act
            ObjectResult? result = sut.Delete(gamenightid) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("GameNight with id 40 not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_Remove_Players_Present()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("a@a.nl")).Returns(GetSingleUser("a@a.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            // Act
            ObjectResult? result = sut.Delete(gamenightid) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Players are already present at gamenight. Gamenights can only be canceled without players.", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_Remove_User_Does_Not_Own()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(2)).Returns(GetSingleGamenight(2));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "b@b.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("b@b.nl")).Returns(GetSingleUser("b@b.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));



            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 2;

            // Act
            ObjectResult? result = sut.Delete(gamenightid) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User does not own gamenight", result.Value.ToString());
        }

        #endregion

        #region Player Requests
        [Fact]
        public void Gamenight_AddPlayer()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            RedirectToActionResult? result = sut.AddPlayer(gamenightid, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Gamenight", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void Gamenight_AddPlayer_User_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "f@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("f@c.nl")).Returns(GetSingleUser("f@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.AddPlayer(gamenightid, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal($"User with email f@c.nl not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_AddPlayer_Gamenight_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(100)).Returns(GetSingleGamenight(100));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 100;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.AddPlayer(gamenightid, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal($"GameNight with id 100 not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_AddPlayer_Already_Plays_Same_Day()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "d@d.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("d@d.nl")).Returns(GetSingleUser("d@d.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.AddPlayer(gamenightid, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal($"Player already subscribed to gamenight on {DateTime.Now.Date}", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_AddPlayer_Not_18_For_Adult_Gamenight()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "e@e.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("e@e.nl")).Returns(GetSingleUser("e@e.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.AddPlayer(gamenightid, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Players under 18 cannot apply for 18+ gamenights.", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_RemovePlayer()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            // Act
            RedirectToActionResult? result = sut.RemovePlayer(gamenightid) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Gamenight", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void Gamenight_RemovePlayer_Player_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "e@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("e@c.nl")).Returns(GetSingleUser("e@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            // Act
            ObjectResult? result = sut.RemovePlayer(gamenightid) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Player not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_RemovePlayer_Gamenight_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(100)).Returns(GetSingleGamenight(100));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 100;

            // Act
            ObjectResult? result = sut.RemovePlayer(gamenightid) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("GameNight with id 100 not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_RemovePlayer_User_Not_In_Gamenight()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "e@e.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("e@e.nl")).Returns(GetSingleUser("e@e.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            // Act
            ObjectResult? result = sut.RemovePlayer(gamenightid) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User not present in gamenight", result.Value.ToString());
        }

        #endregion

        #region Food Requests
        [Fact]
        public void Gamenight_AddFood()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(3)).Returns(GetSingleGamenight(3));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 3;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            RedirectToActionResult? result = sut.AddFoodToGamenight(gamenightid, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Foodstuff", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void Gamenight_AddFood_Gamenight_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(300)).Returns(GetSingleGamenight(300));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 300;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.AddFoodToGamenight(gamenightid, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("GameNight with id 300 not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_AddFood_User_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(3)).Returns(GetSingleGamenight(3));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "d@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("d@c.nl")).Returns(GetSingleUser("d@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 3;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.AddFoodToGamenight(gamenightid, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User with id d@c.nl not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_AddFood_Already_Contributed()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.AddFoodToGamenight(gamenightid, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User with id c@c.nl already has food item in potluck. Update instead.", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_UpdateFood()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                id = 3,
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            RedirectToActionResult? result = sut.UpdateFoodToGamenight(gamenightid, foodstuffs.id, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Foodstuff", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void Gamenight_UpdateFood_Gamenight_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(100)).Returns(GetSingleGamenight(100));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 100;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                id = 3,
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.UpdateFoodToGamenight(gamenightid, foodstuffs.id, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("GameNight with id 100 not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_UpdateFood_User_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "d@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("d@c.nl")).Returns(GetSingleUser("d@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                id = 3,
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.UpdateFoodToGamenight(gamenightid, foodstuffs.id, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User with email d@c.nl not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_UpdateFood_User_Doesnt_Own_Item()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                id = 2,
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.UpdateFoodToGamenight(gamenightid, foodstuffs.id, foodstuffs.Name, foodstuffs.Description, foodstuffs.isVegan, foodstuffs.isAlcoholic, foodstuffs.nutAlergy) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User does not own food item!", result.Value.ToString());
        }


        [Fact]
        public void Gamenight_RemoveFood()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                id = 3,
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            RedirectToActionResult? result = sut.RemoveFoodFromGamenight(foodstuffs.id, gamenightid) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Foodstuff", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void Gamenight_RemoveFood_Gamenight_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(100)).Returns(GetSingleGamenight(100));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 100;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                id = 3,
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.RemoveFoodFromGamenight(foodstuffs.id, gamenightid) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("GameNight with id 100 not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_RemoveFood_User_Not_Found()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "d@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("d@c.nl")).Returns(GetSingleUser("d@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                id = 3,
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.RemoveFoodFromGamenight(foodstuffs.id, gamenightid) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User with email d@c.nl not found", result.Value.ToString());
        }

        [Fact]
        public void Gamenight_RemoveFood_User_Doesnt_Own_Item()
        {
            // Arrange
            Mock<IGamenightRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGamenights());
            mock.Setup(x => x.GetSingleGamenight(1)).Returns(GetSingleGamenight(1));

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "c@c.nl")
            }, "mock"));

            Mock<IUserRepository> mockusers = new();
            mockusers.Setup(x => x.GetAll).Returns(GetUsers());
            mockusers.Setup(x => x.GetSingleUser("c@c.nl")).Returns(GetSingleUser("c@c.nl"));

            Mock<IGameRepository> mockgames = new();
            mockgames.Setup(x => x.GetAll).Returns(GetGames());
            mockgames.Setup(x => x.GetSingleGame(2)).Returns(GetSingleGame(2));

            GamenightController gamenightController = new GamenightController(loggerFactory.CreateLogger<GamenightController>(), mock.Object, mockusers.Object, mockgames.Object);
            gamenightController.ControllerContext.HttpContext = new DefaultHttpContext();
            gamenightController.HttpContext.User = userIdentity;
            var sut = gamenightController;

            int gamenightid = 1;

            Foodstuffs foodstuffs = new Foodstuffs()
            {
                id = 2,
                Name = "Name",
                Description = "Description",
                nutAlergy = false,
                isAlcoholic = false,
                isVegan = false,
                userid = 3
            };

            // Act
            ObjectResult? result = sut.RemoveFoodFromGamenight(foodstuffs.id, gamenightid) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User does not own food item!", result.Value.ToString());
        }

        #endregion


        #region Seeding

        public User[] GetUsers()
        {
            return new User[]
                {
                new User { Id = 1, FirstName = "a", LastName = "A", Gender = Gender.Man, Email = "a@a.nl", Birthday = System.DateTime.Now, Name = "", Street = "AS", City = "AC", HouseNumber = 1, HouseNumberAdditions = "", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>(), isVegan = false, nutAlergy = false, toleratesAlcohol = true },
                new User { Id = 2, FirstName = "b", LastName = "B", Gender = Gender.Woman, Email = "b@b.nl", Birthday = System.DateTime.Today, Name = "", Street = "BS", City = "BC", HouseNumber = 2, HouseNumberAdditions = "", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>(), isVegan = true, nutAlergy = false, toleratesAlcohol = true },
                new User { Id = 3, FirstName = "c", LastName = "C", Gender = Gender.NonBinary, Email = "c@c.nl", Birthday = System.DateTime.Now.AddYears(-30), Name = "", Street = "CS", City = "CC", HouseNumber = 3, HouseNumberAdditions = "a", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>(), isVegan = false, nutAlergy = true, toleratesAlcohol = true },
                new User { Id = 4, FirstName = "d", LastName = "D", Gender = Gender.Other, Email = "d@d.nl", Birthday = System.DateTime.Today, Name = "Building 1", Street = "DS", City = "DC", HouseNumber = 4, HouseNumberAdditions = "", playerat = new List<GameNight>()
                {
                    new GameNight { Id = 5, DateTime = DateTime.Now}
                }, playergames = new List<PlayerGamenight>(), isVegan = false, nutAlergy = false, toleratesAlcohol = false },
                new User { Id = 5, FirstName = "e", LastName = "E", Gender = Gender.Man, Email = "e@e.nl", Birthday = System.DateTime.Now, Name = "Building 2", Street = "ES", City = "EC", HouseNumber = 5, HouseNumberAdditions = "b", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>(), isVegan = false, nutAlergy = false, toleratesAlcohol = true },
                };
        }

        public User? GetSingleUser(string email)
        {
            return GetUsers().FirstOrDefault(x => x.Email == email);
        }

        public Game[] GetGames()
        {
            return new Game[]
            {
                new Game(){Id = 1, Name = "a", Description = "a", EighteenPlus = true, GameType = Type.Board, Genre = Genre.TTRPG, PicturePath="a"},
                new Game(){Id = 2, Name = "b", Description = "b", EighteenPlus = false, GameType = Type.Long_term, Genre = Genre.Social_deduction, PicturePath = "b"},
                new Game(){Id = 3, Name = "c", Description = "c", EighteenPlus = false, GameType = Type.Card, Genre = Genre.Resource_management, PicturePath = "c"},
                new Game(){Id = 4, Name = "d", Description = "d", EighteenPlus = false, GameType = Type.Long_term, Genre = Genre.Social_deduction, PicturePath = "d"},
                new Game(){Id = 5, Name = "e", Description = "e", EighteenPlus = true, GameType = Type.Card, Genre = Genre.Coöperation, PicturePath = "e"}
            };
        }

        public Game? GetSingleGame(int id)
        {
            return GetGames().FirstOrDefault(x => x.Id == id);
        }

        public GameNight[] GetGamenights()
        {
            return new GameNight[]
            {
                new GameNight(){Id = 1, OrganizerID = 1, GameID = 1, DateTime = System.DateTime.Now, isEighteenPlus = false, isPotluck = false, maxPlayers = 2, Name = "", Street = "AS", City = "AC", HouseNumber = 1, HouseNumberAdditions = "",
                Organizer =                 new User { Id = 1, FirstName = "a", LastName = "A", Gender = Gender.Man, Email = "a@a.nl", Birthday = System.DateTime.Now, Name = "", Street = "AS", City = "AC", HouseNumber = 1, HouseNumberAdditions = "", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>(), isVegan = false, nutAlergy = false, toleratesAlcohol = true },
                Players = new List<User>(){
                new User { Id = 2, FirstName = "b", LastName = "B", Gender = Gender.Woman, Email = "b@b.nl", Birthday = System.DateTime.Today, Name = "", Street = "BS", City = "BC", HouseNumber = 2, HouseNumberAdditions = "", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>(), isVegan = true, nutAlergy = false, toleratesAlcohol = true },
                new User { Id = 3, FirstName = "c", LastName = "C", Gender = Gender.NonBinary, Email = "c@c.nl", Birthday = System.DateTime.Now, Name = "", Street = "CS", City = "CC", HouseNumber = 3, HouseNumberAdditions = "a", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>(), isVegan = false, nutAlergy = true, toleratesAlcohol = true },
                new User { Id = 4, FirstName = "d", LastName = "D", Gender = Gender.Other, Email = "d@d.nl", Birthday = System.DateTime.Today, Name = "Building 1", Street = "DS", City = "DC", HouseNumber = 4, HouseNumberAdditions = "", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>(), isVegan = false, nutAlergy = false, toleratesAlcohol = false }},
                Food = new List<Foodstuffs>(){
                    new Foodstuffs(){id = 1, Name = "a", Description = "A", userid = 1, isAlcoholic = false, isVegan = false, nutAlergy = false},
                    new Foodstuffs(){id = 2, Name = "b", Description = "B", userid = 2, isAlcoholic = true, isVegan = false, nutAlergy = false},
                    new Foodstuffs(){id = 3, Name = "c", Description = "C", userid = 3, isAlcoholic = false, isVegan = true, nutAlergy = false},
                    new Foodstuffs(){id = 4, Name = "d", Description = "D", userid = 4, isAlcoholic = false, isVegan = false, nutAlergy = true},

                },
                PlayedGame = new Game(){Id = 1, Name = "a", Description = "a", EighteenPlus = true, GameType = Type.Board, Genre = Genre.TTRPG, PicturePath="a"}},

                new GameNight(){Id = 2, OrganizerID = 1, GameID = 1, DateTime = System.DateTime.Now, isEighteenPlus = false, isPotluck = false, maxPlayers = 2, Name = "", Street = "AS", City = "AC", HouseNumber = 1, HouseNumberAdditions = "",
                Organizer = new User { Id = 1, FirstName = "a", LastName = "A", Gender = Gender.Man, Email = "a@a.nl", Birthday = System.DateTime.Now, Name = "", Street = "AS", City = "AC", HouseNumber = 1, HouseNumberAdditions = "", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>(), isVegan = false, nutAlergy = false, toleratesAlcohol = true },
                Players = new List<User>(){},
                Food = new List<Foodstuffs>(){}
                },

                new GameNight(){Id = 3, OrganizerID = 1, GameID = 1, DateTime = System.DateTime.Now, isEighteenPlus = false, isPotluck = false, maxPlayers = 2, Name = "", Street = "AS", City = "AC", HouseNumber = 1, HouseNumberAdditions = "",
                Organizer = new User { Id = 1, FirstName = "a", LastName = "A", Gender = Gender.Man, Email = "a@a.nl", Birthday = System.DateTime.Now, Name = "", Street = "AS", City = "AC", HouseNumber = 1, HouseNumberAdditions = "", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>(), isVegan = false, nutAlergy = false, toleratesAlcohol = true },
                Players = new List<User>(){
                    new User { Id = 3, FirstName = "c", LastName = "C", Gender = Gender.NonBinary, Email = "c@c.nl", Birthday = System.DateTime.Now, Name = "", Street = "CS", City = "CC", HouseNumber = 3, HouseNumberAdditions = "a", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>(), isVegan = false, nutAlergy = true, toleratesAlcohol = true },
                },
                Food = new List<Foodstuffs>(){}
                }
            };
        }

        public GameNight? GetSingleGamenight(int id)
        {
            return GetGamenights().FirstOrDefault(x => x.Id == id);
        }
        #endregion


    }
}

