using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Models
{
    public class GameTests
    {
        [Fact]
        public void Game_Object_Creation()
        {
            // Arrange
            const int ExId = 5;
            const string ExName = "Name";
            const string ExDescription = "Description";
            const bool ExEighteenplus = false;
            const Domain.Models.Type ExType = Domain.Models.Type.Card;
            const Genre ExGenre = Genre.Puzzle;

            // Act
            Game testgame = new Game()
            {
                Id = ExId,
                Name = ExName,
                Description = ExDescription,
                GameType = ExType,
                Genre = Genre.Puzzle,
                EighteenPlus = ExEighteenplus,
                PicturePath = ""
            };

            // Assert
            Assert.Equal(testgame.Id, ExId);
            Assert.Equal(testgame.Name, ExName);
            Assert.Equal(testgame.Description, ExDescription);
            Assert.Equal(testgame.GameType, ExType);
            Assert.Equal(testgame.Genre, ExGenre);

            Assert.False(testgame.EighteenPlus);

            Assert.Empty(testgame.PicturePath);
        }
    }
}
