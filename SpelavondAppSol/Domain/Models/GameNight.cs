using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GameNight
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int OrganizerID { get; set; }
        public User Organizer { get; set; }

        #region Playadress
        public string? Name { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string? HouseNumberAdditions { get; set; }
        public string City { get; set; }
        #endregion

        public int maxPlayers { get; set; }

        public ICollection<User> Players { get; set; }
        [JsonIgnore]
        public List<PlayerGamenight> PlayersAdditions { get; set;}

        public DateTime DateTime { get; set; }

        [JsonIgnore]
        public int GameID { get; set; }
        public Game PlayedGame { get; set; }
        public bool isPotluck { get; set; }
        public bool isEighteenPlus { get; set; }
        public ICollection<Foodstuffs> Food { get; set; }

        public bool IsFull()
        {
            if (maxPlayers > Players.Count())
            {
                return false;
            }
            return true;
        }

        public void UpdateFromForm(GameNight gameNight)
        {
            Name = gameNight.Name;
            Street = gameNight.Street;
            HouseNumber = gameNight.HouseNumber;
            HouseNumberAdditions = gameNight.HouseNumberAdditions;
            City = gameNight.City;
            isEighteenPlus = gameNight.isEighteenPlus;
            isPotluck = gameNight.isPotluck;
            DateTime = gameNight.DateTime;
            GameID = gameNight.GameID;
            PlayedGame = gameNight.PlayedGame;
            maxPlayers = gameNight.maxPlayers;
        }
    }
}
