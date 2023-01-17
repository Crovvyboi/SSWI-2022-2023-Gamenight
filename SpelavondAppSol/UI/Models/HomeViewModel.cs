using Domain.Models;

namespace UI.Models
{
    public class HomeViewModel
    {
        public ICollection<GameNight> gameNights { get; set; }
        public User? user { get; set;}

        public HomeViewModel(ICollection<GameNight> gameNights, User user)
        {
            this.gameNights = gameNights;
            this.user = user;
        }

        public HomeViewModel(ICollection<GameNight> gameNights)
        {
            this.gameNights = gameNights;
            user = null;
        }
    }
}
