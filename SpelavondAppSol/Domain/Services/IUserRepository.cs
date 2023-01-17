using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUserRepository
    {
        IEnumerable<User>? GetAll { get; }

        User? GetSingleUser(string email);
        void Create(User user);
        void Update(int id, User user);
        void Delete(int id);
    }
}
