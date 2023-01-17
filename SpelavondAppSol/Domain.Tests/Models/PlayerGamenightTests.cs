using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Models
{
    public class PlayerGamenightTests
    {
        [Fact]
        public void PlayerGamenight_Object_Creation ()
        {
            // Arrange
            const int ExId = 5;
            const int ExGamenightId = 2;
            const int ExPlayerId = 7;
            
            // Act
            PlayerGamenight playerGamenight = new PlayerGamenight()
            {
                Id = ExId,
                gameNightID = ExGamenightId,
                playerID = ExPlayerId
            };

            // Assert
            Assert.Equal(playerGamenight.Id, ExId);
            Assert.Equal(playerGamenight.gameNightID, ExGamenightId);
            Assert.Equal(playerGamenight.playerID, ExPlayerId);
        }
    }
}
