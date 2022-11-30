using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MushroomWebsite.Repository.IRepository;
using MushroomWebsite.Data;
using MushroomWebsite.Models;
using System.Net.Mail;
using System.Net;

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
            Entry = new EntryRepository(_db);
        }
        public IMushroomRepository Mushroom { get; set; }
        public IUserRepository User { get; set; }
        public IRoleRepository Role { get; set; }
        public IEntryRepository Entry { get; set; }

        public void RegisterAndNotifyUser(User user)
        {
            User.Add(user);
            SendEmail(user.Email);
        }

        private void SendEmail(string email)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mailpamiw@gmail.com", "hitsfpwxkgygrbzt"),
                EnableSsl = true
                // specify whether your host accepts SSL connections
            };

            try
            {
                client.Send("mailpamiw@gmail.com", email, "Hello", "Welcome new user of Mushrooms' Forum!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in SendEmail: {0}",
                    ex.ToString());
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
