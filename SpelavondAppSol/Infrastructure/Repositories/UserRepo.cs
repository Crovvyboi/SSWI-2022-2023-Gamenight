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
    public class UserRepo : IUserRepository
    {
        private readonly DatabaseContext _dbContext;

        public UserRepo(DatabaseContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public IEnumerable<User>? GetAll => _dbContext.Users
                .Include(x => x.playergames)
                .ThenInclude(x => x.GameNight)
                .ThenInclude(x => x.PlayedGame)
                .ToList();

        public User? GetSingleUser(string email)
        {
            return _dbContext.Users
                .Include(x => x.playergames)
                .ThenInclude(x => x.GameNight)
                .ThenInclude(x => x.PlayedGame)
                .ToList()
                .FirstOrDefault(r => r.Email == email);
        }

        public void Create(User newuser)
        {
            _dbContext.Users.Add(newuser);
            _dbContext.SaveChanges();
        }

        public void Update(int id, User updateuser)
        {
            var entityToUpdate = _dbContext.Users.FirstOrDefault(r => r.Id == id);
            if (entityToUpdate != null)
            {
                entityToUpdate = updateuser;
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var entityToUpdate = _dbContext.Users.FirstOrDefault(r => r.Id == id);
            if (entityToUpdate != null)
            {
                _dbContext.Users.Remove(entityToUpdate);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(string email)
        {
            var entityToUpdate = _dbContext.Users.FirstOrDefault(r => r.Email == email);
            if (entityToUpdate != null)
            {
                _dbContext.Users.Remove(entityToUpdate);
                _dbContext.SaveChanges();
            }
        }
    }
}
