using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Models
{
    public class GameNightTests
    {
        [Fact]
        public void Gamenight_Object_Creation()
        {
            // Arrange
            const int ExId = 5;
            const int ExGameId = 6;
            DateTime ExDateTime = DateTime.Now;
            const int ExOrganizerId = 4;

            const string ExCity = "City";
            const string ExStreet = "Street";
            const int ExHouseNumber = 2;

            const bool ExisPotluck = true;
            const int ExMaxPlayers = 3;

            // Act
            GameNight gameNight = new GameNight()
            {
                Id = ExId,
                GameID = ExGameId,
                DateTime = ExDateTime,
                OrganizerID = ExOrganizerId,

                Name = "",
                City = ExCity,
                Street = ExStreet,
                HouseNumber = ExHouseNumber,
                HouseNumberAdditions = "",

                isPotluck = ExisPotluck,
                maxPlayers = ExMaxPlayers,

                Food = new List<Foodstuffs>(),
                Players = new List<User>()
            };

            // Assert
            Assert.Equal(gameNight.Id, ExId);
            Assert.Equal(gameNight.GameID, ExGameId);
            Assert.Equal(gameNight.DateTime, ExDateTime);
            Assert.Equal(gameNight.OrganizerID, ExOrganizerId);
            Assert.Equal(gameNight.City, ExCity);
            Assert.Equal(gameNight.Street, ExStreet);
            Assert.Equal(gameNight.HouseNumber, ExHouseNumber);

            Assert.True(gameNight.isPotluck);

            Assert.Empty(gameNight.Food);
            Assert.Empty(gameNight.Players);
            Assert.Empty(gameNight.HouseNumberAdditions);
            Assert.Empty(gameNight.Name);
        }

        [Fact]
        public void Gamenight_Is_Full()
        {
            // Arrange
            GameNight gameNight = new GameNight()
            {
                maxPlayers = 2,
                Players = new List<User>()
                {
                    new User(),
                    new User()
                }
            };

            // Act
            bool result = gameNight.IsFull();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Gamenight_Is_Not_Full()
        {
            // Arrange
            GameNight gameNight = new GameNight()
            {
                maxPlayers = 4,
                Players = new List<User>()
                {
                    new User(),
                    new User()
                }
            };

            // Act
            bool result = gameNight.IsFull();

            // Assert
            Assert.False(result);
        }
    }
}
