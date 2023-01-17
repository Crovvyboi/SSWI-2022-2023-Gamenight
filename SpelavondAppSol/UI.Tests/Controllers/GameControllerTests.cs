using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Controllers;
using Type = Domain.Models.Type;

namespace UI.Tests.Controllers
{
    public class GameControllerTests
    {
        private LoggerFactory loggerFactory = new LoggerFactory();

        [Fact]
        public void Can_Use_Repo()
        {
            // Arrange
            Mock<IGameRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GameController(loggerFactory.CreateLogger<GameController>(), mock.Object);

            // Act
            IEnumerable<Game>? result = (sut.GetAll() as OkObjectResult)?.Value as List<Game>;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);

        }

        [Fact]
        public void GetAll_Returns_All()
        {
            // Arrange
            Mock<IGameRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GameController(loggerFactory.CreateLogger<GameController>(), mock.Object);

            // Act
            IEnumerable<Game>? result = (sut.GetAll() as OkObjectResult)?.Value as List<Game>;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetSingleGame_Returns_Nothing()
        {
            // Arrange
            Mock<IGameRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GameController(loggerFactory.CreateLogger<GameController>(), mock.Object);

            int gameid = 500;

            // Act
            ObjectResult? result = sut.GetSingleGame(gameid) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);

        }

        [Fact]
        public void GetSingleGame_Returns_Single_Game()
        {
            // Arrange
            Mock<IGameRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GameController(loggerFactory.CreateLogger<GameController>(), mock.Object);

            int gameid = 2;

            // Act
            Game? result = (sut.GetSingleGame(gameid) as ObjectResult)?.Value as Game;

            // Assert
            Assert.Equal(gameid, result.Id);
        }

        [Fact]
        public void CreateGame_Game_Is_Null()
        {
            // Arrange
            Mock<IGameRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GameController(loggerFactory.CreateLogger<GameController>(), mock.Object);

            Game? newgame = null;

            // Act
            ObjectResult? result = sut.Create(newgame) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void CreateGame()
        {
            // Arrange
            Mock<IGameRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GameController(loggerFactory.CreateLogger<GameController>(), mock.Object);

            Game newgame = new Game();

            // Act
            ObjectResult? result = sut.Create(newgame) as ObjectResult;

            // Assert
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public void UpdateGame_Game_Is_Null()
        {
            // Arrange
            Mock<IGameRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GameController(loggerFactory.CreateLogger<GameController>(), mock.Object);

            Game? updategame = null;

            // Act
            ObjectResult? result = sut.Update(1, updategame) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void UpdateGame_Game_Not_Found()
        {
            // Arrange
            Mock<IGameRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GameController(loggerFactory.CreateLogger<GameController>(), mock.Object);

            Game updategame = new Game()
            {
                Id = 500,
                Name = "Updated"
            };

            // Act
            ObjectResult? result = sut.Update(updategame.Id, updategame) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void UpdateGame()
        {
            // Arrange
            Mock<IGameRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GameController(loggerFactory.CreateLogger<GameController>(), mock.Object);

            Game updategame = new Game()
            {
                Id = 2,
                Name = "Updated"
            };

            // Act
            ObjectResult? result = sut.Update(updategame.Id ,updategame) as ObjectResult;

            // Assert
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void DeleteGame_Game_Not_Found()
        {
            // Arrange
            Mock<IGameRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GameController(loggerFactory.CreateLogger<GameController>(), mock.Object);

            int gameid = 500;

            // Act
            ObjectResult? result = sut.Delete(gameid) as ObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void DeleteGame()
        {
            // Arrange
            Mock<IGameRepository> mock = new();
            mock.Setup(x => x.GetAll).Returns(GetGames());

            var sut = new GameController(loggerFactory.CreateLogger<GameController>(), mock.Object);

            int gameid = 2;

            // Act
            ObjectResult? result = sut.Delete(gameid) as ObjectResult;

            // Assert
            Assert.Equal(204, result.StatusCode);
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
    }
}
