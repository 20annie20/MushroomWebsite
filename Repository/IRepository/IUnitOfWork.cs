using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MushroomWebsite.Models;

namespace MushroomWebsite.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IMushroomRepository Mushroom { get; }
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        void Save();
        void RegisterAndNotifyUser(User obj);
    }
}
