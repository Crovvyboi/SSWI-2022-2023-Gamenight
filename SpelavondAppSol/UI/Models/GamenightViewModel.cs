using Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Models
{
    public class GamenightViewModel : PageModel
    {
        public GameNight gamenight;
        public User? user;

        public GamenightViewModel(GameNight gameNight, User user)
        {
            gamenight = gameNight;
            this.user = user;
        }
        public GamenightViewModel(GameNight gameNight)
        {
            gamenight = gameNight;
            user = null;
        }
    }
}
