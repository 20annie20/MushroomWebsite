using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MushroomWebsite.Models;

namespace MushroomWebsite.Repository.IRepository
{
    public interface IEntryRepository : IRepository<Entry>
    {
        void Update(Entry obj);
    }
}
