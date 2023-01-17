using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IGameRepository
    {
        IEnumerable<Game>? GetAll { get; }

        Game? GetSingleGame(int id);
        void Create(Game game);
        void Update(int id, Game game);
        void Delete(int id);
    }
}
