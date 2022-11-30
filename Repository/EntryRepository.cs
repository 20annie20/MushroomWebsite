using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MushroomWebsite.Repository.IRepository;
using MushroomWebsite.Models;
using MushroomWebsite.Data;
using Microsoft.EntityFrameworkCore;

namespace MushroomWebsite.Repository
{
    public class EntryRepository : Repository<Entry>, IEntryRepository
    {
        private ApplicationDbContext _db;
        internal DbSet<Article> dbArticleSet;
        internal DbSet<User> dbUserSet;

        public EntryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            this.dbArticleSet = _db.Set<Article>();
            this.dbUserSet = _db.Set<User>();
        }

        public void Update(Entry obj)
        {
            _db.Entries.Update(obj);
        }

        public new IEnumerable<Entry> GetAll()
        {
            IQueryable<Entry> query = dbSet;
            var Entries = query.ToList();
            IQueryable<Article> queryArticle = dbArticleSet;
            IQueryable<User> queryUser = dbUserSet;

            foreach (var s in Entries)
            {
                
                var Article = queryArticle.FirstOrDefault(c => c.EntryId == s.Id);
                var User = queryUser.FirstOrDefault(c => c.Id == s.UserId);
                //s.Article = Article;
                //s.User = User;
            }

            return Entries;
        }
    }
}
