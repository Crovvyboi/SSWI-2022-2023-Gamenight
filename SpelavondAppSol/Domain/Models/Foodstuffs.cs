using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Foodstuffs
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool nutAlergy { get; set; }
        public bool isVegan { get; set; }
        public bool isAlcoholic { get; set; }


        public int userid { get; set; }
        public User BroughtBy { get; set; }

    }
}
