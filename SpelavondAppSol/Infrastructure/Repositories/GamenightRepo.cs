using Domain.Models;
using Domain.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GamenightRepo : IGamenightRepository
    {
        private readonly DatabaseContext _dbContext;

        public GamenightRepo(DatabaseContext dbcontext)
        {
            _dbContext = dbcontext;
        }


        public IEnumerable<GameNight>? GetAll => _dbContext.GameNight
                .Include(game => game.PlayedGame)
                .Include(game => game.Organizer)
                .Include(food => food.Food)
                .ThenInclude(byplayer => byplayer.BroughtBy)
                .Include(playergame => playergame.Players)
                .ToList();

        public GameNight? GetSingleGamenight(int id)
        {
            GameNight? gamenight = _dbContext.GameNight
                .Include(game => game.PlayedGame)
                .Include(game => game.Organizer)
                .Include(food => food.Food)
                .ThenInclude(byplayer => byplayer.BroughtBy)
                .Include(playergame => playergame.Players)
                .FirstOrDefault(game => game.Id == id);

            return gamenight;
        }

        public IEnumerable<GameNight>? GetAllOrganized(int userid)
        {
            return _dbContext.GameNight
                .Include(game => game.PlayedGame)
                .Include(game => game.Organizer)
                .Include(food => food.Food)
                .ThenInclude(byplayer => byplayer.BroughtBy)
                .Include(playergame => playergame.Players)
                .Where(game => game.Organizer.Id == userid)
                .ToList();
        }

        public void Create(GameNight newgamenight)
        {
            _dbContext.GameNight.Add(newgamenight);
            _dbContext.SaveChanges();
        }

        public void Update(int id, GameNight updategamenight)
        {
            var entityToUpdate = _dbContext.GameNight
                .Include(game => game.PlayedGame)
                .Include(game => game.Organizer)
                .Include(food => food.Food)
                .ThenInclude(byplayer => byplayer.BroughtBy)
                .Include(playergame => playergame.Players)
                .FirstOrDefault(game => game.Id == updategamenight.Id);
            if (entityToUpdate != null)
            {
                entityToUpdate = updategamenight;
                _dbContext.Update(entityToUpdate);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            GameNight? entityToUpdate = _dbContext.GameNight
                .Include(game => game.PlayedGame)
                .Include(game => game.Organizer)
                .Include(food => food.Food)
                .ThenInclude(byplayer => byplayer.BroughtBy)
                .Include(playergame => playergame.Players)
                .FirstOrDefault(game => game.Id == id);

            if (entityToUpdate != null)
            {
                // _dbContext.userGamenights.RemoveRange(entityToUpdate.PlayersAdditions);
                _dbContext.GameNight.Remove(entityToUpdate);
                _dbContext.SaveChanges();
            }
        }

        public void AddPlayerToGameNight(User newplayer, int gamenightid)
        {
            // get gamenight
            var gamenight = _dbContext.GameNight.FirstOrDefault(id => id.Id == gamenightid);
            if (gamenight != null)
            {
                // check if player can be added (isfull / 18+)
                if (gamenight.Players.Contains(newplayer))
                {
                    return;
                }
                if (gamenight.PlayedGame.EighteenPlus && !newplayer.isEighteen())
                {
                    return;
                }

                // if okay, insert player into playerlist gamenight
                gamenight.Players.Add(newplayer);

                // Call update function
                Update(gamenightid, gamenight);
            }


        }

        public void RemovePlayerFromGameNight(int playerid, GameNight gamenight)
        {
            // get player
            var player = _dbContext.Users.FirstOrDefault(player => player.Id == playerid);
            if (player != null)
            {

                // check if gamenight has this player
                if (gamenight.Players.Contains(player))
                {
                    // if okay, remove player from playerlist gamenight
                    gamenight.Players.Remove(player);

                    // update gamenight to updated gamenight
                    Update(gamenight.Id, gamenight);
                }

                
            }
        }

        public void AddFoodstuffToGameNight(Foodstuffs foodstuffs, GameNight gamenight)
        {

            // check if list already contains fooditem of user
            if (gamenight.Food.FirstOrDefault(x => x.userid == foodstuffs.userid) == null)
            {
                // insert foodstuff to the list
                gamenight.Food.Add(foodstuffs);

                // update gamenight to updated gamenight
                Update(gamenight.Id, gamenight);
            }
        }

        public void RemoveFoodstuffFromGameNight(int foodstuffid, GameNight gamenight)
        {

            var food = gamenight.Food.FirstOrDefault(x => x.id == foodstuffid);
            // check if gamenight foodlist contains foodstuff by id
            if (food != null)
            {
                // if yes, remove foodstuff from list by id
                gamenight.Food.Remove(food);
                // save changes
                Update(gamenight.Id, gamenight);
            }
        }

        public void UpdateFoodstuffInGamenight(int foodstuffid, Foodstuffs updatedfoodstuffs, GameNight gamenight)
        {

            // check foodstuff presence by getting
            var food = gamenight.Food.FirstOrDefault(x => x.id == foodstuffid);
            if (food != null)
            {
                // if yes, update foodstuff with updated foodstuff
                gamenight.Food.Remove(food);
                gamenight.Food.Add(updatedfoodstuffs);

                // update gamenight to updated gamenight
                Update(gamenight.Id, gamenight);
            }

        }


    }
}
