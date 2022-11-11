using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MushroomWebsite.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IMushroomRepository Mushroom { get; }

        void Save();
    }
}
