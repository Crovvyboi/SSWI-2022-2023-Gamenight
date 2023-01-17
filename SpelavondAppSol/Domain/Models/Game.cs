using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public Type GameType { get; set; }
        public bool EighteenPlus { get; set; }
        public string PicturePath { get; set; }

    }

    public enum Genre
    {
        Party,
        Puzzle,
        Strategy,
        TTRPG,
        Coöperation,
        Resource_management,
        Social_deduction
    }

    public enum Type
    {
        Card,
        Board,
        Long_term
    }
}
