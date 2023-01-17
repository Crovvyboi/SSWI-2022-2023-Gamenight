using Microsoft.EntityFrameworkCore;
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
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }


        #region HomeAdress
        public string? Name { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string? HouseNumberAdditions { get; set; }
        public string City { get; set; }
        #endregion


        public DateTime Birthday { get; set; }

        [JsonIgnore]
        public ICollection<GameNight> playerat { get; set; }
        [JsonIgnore]
        public List<PlayerGamenight> playergames { get; set; }

        public bool nutAlergy { get; set; }
        public bool isVegan { get; set; }
        public bool toleratesAlcohol { get; set; }


        public bool isEighteen()
        {
            if (Birthday.AddYears(18) >= DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }

    public enum Gender
    {
        Man,
        Woman,
        NonBinary,
        Other
    }
}
