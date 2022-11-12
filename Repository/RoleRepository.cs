using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MushroomWebsite.Repository.IRepository;
using MushroomWebsite.Models;
using MushroomWebsite.Data;

namespace MushroomWebsite.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private ApplicationDbContext _db;

        public RoleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
