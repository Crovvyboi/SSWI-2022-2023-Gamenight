using Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Models
{
    public class EditGamenightViewModel : PageModel
    {
        public List<Game> _games;
        public GameNight _gameNight;
        public User _user;

        public EditGamenightViewModel(List<Game> games, GameNight gameNight, User user)
        {
            _gameNight = gameNight;
            _games = games;
            _user = user;   
        }
    }
}
