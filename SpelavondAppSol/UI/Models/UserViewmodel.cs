using Domain.Models;

namespace UI.Models
{
    public class UserViewmodel
    {
        public User? _user { get; set; }
        public ICollection<GameNight>? _gameNights { get; set; }

        public UserViewmodel(User? user, ICollection<GameNight> gameNights)
        {
            _user = user;
            _gameNights = gameNights;
        }
    }
}
