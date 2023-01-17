using Domain.Models;

namespace UI.Models
{
    public class FoodstuffViewmodel
    {
        public List<Foodstuffs> _food { get; set; }
        public GameNight _gamenightid { get; set; }

        
        public string? inputname { get; set; }
        public string? inputdesc { get; set; }

        public FoodstuffViewmodel(List<Foodstuffs> foodstuffs, GameNight gamenightid)
        {
            _food = foodstuffs;
            _gamenightid = gamenightid;
        }
    }
}
