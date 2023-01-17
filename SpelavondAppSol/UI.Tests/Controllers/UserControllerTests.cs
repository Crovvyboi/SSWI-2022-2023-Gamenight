using Domain.Models;
using Domain.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using UI.Controllers;

namespace UI.Tests.Controllers
{
    public class UserControllerTests
    {
        private LoggerFactory loggerFactory = new LoggerFactory();

        [Fact]
        public void Can_Use_Repo()
        {
            // Arrange
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var sut = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);

            // Act
            IEnumerable<User>? result = (sut.GetAll() as OkObjectResult)?.Value as List<User>;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);

        }

        #region Get Tests
        [Fact]
        public void GetAll_Returns_All()
        {
            // Arrange
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var sut = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);

            // Act
            IEnumerable<User>? result = (sut.GetAll() as OkObjectResult)?.Value as List<User>;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void UserByEmail_Returns_Nothing()
        {
            // Arrange
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var sut = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);

            // Act
            OkObjectResult? result = sut.GetSingleUser("a@b.nl") as OkObjectResult;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UserByEmail_Returns_User()
        {
            // Arrange
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var sut = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);

            // Act
            ObjectResult? result = sut.GetSingleUser("b@b.nl") as ObjectResult;
            User? userResult = result.Value as User;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(2, userResult.Id);
        }
        #endregion

        #region Post Tests
        [Fact]
        public void Create_User_Incorrect_Input()
        {
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            UserController userController = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            userController.HttpContext.User = userIdentity;
            var sut = userController;

            User user = null;

            // Act
            ObjectResult? result = sut.Create(user) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Create_User_Input_Already_Exists()
        {
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "b@b.nl")
            }, "mock"));

            UserController userController = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            userController.HttpContext.User = userIdentity;
            var sut = userController;

            User user = new User()
            {
                Email = "b@b.nl",
                FirstName = "Firstname"
            };

            // Act
            ObjectResult? result = sut.Create(user) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Create_User()
        {
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            UserController userController = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            userController.HttpContext.User = userIdentity;
            var sut = userController;

            User user = new User()
            {
                FirstName = "Firstname"
            };

            // Act
            ObjectResult? result = sut.Create(user) as ObjectResult;

            // Assert
            Assert.Equal(201, result.StatusCode);
        }
        #endregion

        #region Put Tests
        [Fact]
        public void Update_User_Incorrect_Input()
        {
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "b@b.nl")
            }, "mock"));

            UserController userController = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            userController.HttpContext.User = userIdentity;
            var sut = userController;

            User user = null;

            // Act
            ObjectResult? result = sut.Update(user) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Update_User_Not_Found()
        {
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "b@b.nl")
            }, "mock"));

            UserController userController = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            userController.HttpContext.User = userIdentity;
            var sut = userController;

            User user = new User()
            {
                Id = 50,
                FirstName = "Firstname"
            };

            // Act
            ObjectResult? result = sut.Update(user) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Update_User_Does_Not_Own()
        {
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "a@a.nl")
            }, "mock"));

            UserController userController = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            userController.HttpContext.User = userIdentity;
            var sut = userController;

            User user = new User()
            {
                Id = 2,
                FirstName = "Firstname"
            };

            // Act
            ObjectResult? result = sut.Update(user) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Update_User()
        {
            // Arrange
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "b@b.nl")
            }, "mock"));

            UserController userController = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            userController.HttpContext.User = userIdentity;
            var sut = userController;

            User user = new User()
            {
                Id = 2,
                FirstName = "Firstname"
            };

            // Act
            ObjectResult? result = sut.Update(user) as ObjectResult;

            // Assert
            Assert.Equal(204, result.StatusCode);
        }
        #endregion

        #region Delete Tests
        [Fact]
        public void Delete_User_Not_Found()
        {
            // Arrange
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "b@b.nl")
            }, "mock"));

            UserController userController = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            userController.HttpContext.User = userIdentity;
            var sut = userController;

            // Act
            ObjectResult? result = sut.Delete(500) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Delete_User_Not_Owned()
        {
            // Arrange
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "b@b.nl")
            }, "mock"));

            UserController userController = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            userController.HttpContext.User = userIdentity;
            var sut = userController;

            // Act
            ObjectResult? result = sut.Delete(1) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Delete_User()
        {
            // Arrange
            Mock<IUserRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetUsers());

            var userIdentity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "b@b.nl")
            }, "mock"));

            UserController userController = new UserController(loggerFactory.CreateLogger<UserController>(), mock.Object);
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            userController.HttpContext.User = userIdentity;
            var sut = userController;

            // Act
            ObjectResult? result = sut.Delete(2) as ObjectResult;

            // Assert
            Assert.Equal(204, result.StatusCode);
        }
        #endregion


        public User[] GetUsers()
        {
            return new User[]
                {
                new User { Id = 1, FirstName = "a", LastName = "A", Gender = Gender.Man, Email = "a@a.nl", Birthday = System.DateTime.Now, Name = "", Street = "AS", City = "AC", HouseNumber = 1, HouseNumberAdditions = "", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>() },
                new User { Id = 2, FirstName = "b", LastName = "B", Gender = Gender.Woman, Email = "b@b.nl", Birthday = System.DateTime.Today, Name = "", Street = "BS", City = "BC", HouseNumber = 2, HouseNumberAdditions = "", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>() },
                new User { Id = 3, FirstName = "c", LastName = "C", Gender = Gender.NonBinary, Email = "c@c.nl", Birthday = System.DateTime.Now, Name = "", Street = "CS", City = "CC", HouseNumber = 3, HouseNumberAdditions = "a", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>() },
                new User { Id = 4, FirstName = "d", LastName = "D", Gender = Gender.Other, Email = "d@d.nl", Birthday = System.DateTime.Today, Name = "Building 1", Street = "DS", City = "DC", HouseNumber = 4, HouseNumberAdditions = "", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>() },
                new User { Id = 5, FirstName = "e", LastName = "E", Gender = Gender.Man, Email = "e@e.nl", Birthday = System.DateTime.Now, Name = "Building 2", Street = "ES", City = "EC", HouseNumber = 5, HouseNumberAdditions = "b", playerat = new List<GameNight>(), playergames = new List<PlayerGamenight>() },
                };
        }

    }
}
