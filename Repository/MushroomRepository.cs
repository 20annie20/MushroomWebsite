using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MushroomWebsite.Repository. IRepository;
using MushroomWebsite.Models;
using MushroomWebsite.Data;

namespace MushroomWebsite.Repository
{
    public class MushroomRepository : Repository<Mushroom>, IMushroomRepository
    {
        private ApplicationDbContext _db;
        public MushroomRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Mushroom obj)
        {
            _db.Mushrooms.Update(obj);
        }
    }
}
