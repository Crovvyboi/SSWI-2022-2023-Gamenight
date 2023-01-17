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
    public class GameRepo : IGameRepository
    {
        private readonly DatabaseContext _dbContext;

        public GameRepo(DatabaseContext dbcontext)
        {
            _dbContext = dbcontext;
        }


        public IEnumerable<Game>? GetAll => _dbContext.Game.ToList();

        public Game? GetSingleGame(int id)
        {
            return _dbContext.Game.ToList().FirstOrDefault(r => r.Id == id);
        }

        public void Create(Game newgame)
        {
            _dbContext.Game.Add(newgame);
            _dbContext.SaveChanges();
        }

        public void Update(int id, Game updategame)
        {
            var entityToUpdate = _dbContext.Game.FirstOrDefault(r => r.Id == id);
            if (entityToUpdate != null)
            {
                entityToUpdate = updategame;
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var entityToUpdate = _dbContext.Game.FirstOrDefault(r => r.Id == id);
            if (entityToUpdate != null)
            {
                _dbContext.Game.Remove(entityToUpdate);
                _dbContext.SaveChanges();
            }
        }
    }
}
