using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PlayerGamenight
    {
        [Key]
        public int Id { get; set; }

        public int gameNightID { get; set; }
        public GameNight GameNight { get; set; }

        public int playerID { get; set; }
        public User Player { get; set; }
    }
}
