using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MushroomWebsite.Repository.IRepository;
using MushroomWebsite.Models;
using MushroomWebsite.Data;

namespace MushroomWebsite.Repository
{
    public class EntryRepository : Repository<Entry>, IEntryRepository
    {
        private ApplicationDbContext _db;
        public EntryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Entry obj)
        {
            _db.Entries.Update(obj);
        }
    }
}
