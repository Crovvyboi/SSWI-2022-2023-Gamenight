using Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Models
{
    public class NewGamenightViewmodel : PageModel
    {
        public ICollection<Game> _games { get; set; }
        public User _user { get; set; }


        public NewGamenightViewmodel(ICollection<Game> games, User user)
        {
            _games = games;
            _user = user;
        }

    }
}
