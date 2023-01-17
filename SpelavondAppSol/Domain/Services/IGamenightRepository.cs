using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IGamenightRepository
    {
        IEnumerable<GameNight>? GetAll { get; }

        GameNight? GetSingleGamenight(int id);
        IEnumerable<GameNight>? GetAllOrganized(int userid);

        void Create(GameNight game);
        void Update(int id, GameNight game);
        void Delete(int id);

        void AddPlayerToGameNight(User newplayer, int gamenightid);
        void RemovePlayerFromGameNight(int playerid, GameNight gamenightid);

        void AddFoodstuffToGameNight(Foodstuffs foodstuffs, GameNight gamenight);
        void RemoveFoodstuffFromGameNight(int foodstuffid, GameNight gamenight);
        void UpdateFoodstuffInGamenight(int foodstuffid, Foodstuffs updatedfoodstuffs, GameNight gamenight);

    }
}
