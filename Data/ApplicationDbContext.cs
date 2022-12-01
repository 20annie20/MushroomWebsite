using Microsoft.EntityFrameworkCore;
using MushroomWebsite.Models;
using System;

namespace MushroomWebsite.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Article> Articles { get; set; }
        public DbSet<Mushroom> Mushrooms { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }

        public DbSet<EntryMushroom> EntryMushrooms { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, Action<ApplicationDbContext, ModelBuilder> modelCustomizer = null) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(c => c.Email).IsRequired();
            modelBuilder.Entity<User>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<User>().Property(c => c.PasswordHash).IsRequired();
            modelBuilder.Entity<Role>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Mushroom>().Property(c => c.Name).IsRequired();

            modelBuilder.Entity<EntryMushroom>()
                .HasKey(em => new { em.EntryId, em.MushroomId });
            modelBuilder.Entity<EntryMushroom>()
                .HasOne(em => em.Entry)
                .WithMany(e => e.EntryMushrooms)
                .HasForeignKey(em => em.EntryId);
            modelBuilder.Entity<EntryMushroom>()
                .HasOne(em => em.Mushroom)
                .WithMany(m => m.EntryMushrooms)
                .HasForeignKey(em => em.MushroomId);

        }
    }
}
