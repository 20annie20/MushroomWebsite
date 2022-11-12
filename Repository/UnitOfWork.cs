using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MushroomWebsite.Repository.IRepository;
using MushroomWebsite.Data;
using MushroomWebsite.Models;

namespace MushroomWebsite.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Mushroom = new MushroomRepository(_db);
            User = new UserRepository(_db);
            Role = new RoleRepository(_db);
        }
        public IMushroomRepository Mushroom { get; set; }
        public IUserRepository User { get; set; }
        public IRoleRepository Role { get; set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
