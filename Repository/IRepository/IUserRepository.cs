using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MushroomWebsite.Models;

namespace MushroomWebsite.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User obj);

    }
}
