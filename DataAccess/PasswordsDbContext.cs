using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace PasswordsManager.DataAccess
{
    public class PasswordsDbContext : DbContext
    {

        private static PasswordsDbContext _context = null;
        public static PasswordsDbContext Current
        {
            get
            {
                if (_context == null)
                    throw new Exception("Le contexte de base de données n'est pas initialisé !");
                return _context;
            }
        }
        public static async Task<PasswordsDbContext> Initialize()
        {
            if (_context == null)
            {
                _context = new PasswordsDbContext("passwords.db");
                await _context.Database.MigrateAsync();
            }
            return _context;
        }


        internal PasswordsDbContext(DbContextOptions options) : base(options) { }
        private PasswordsDbContext(string databasePath) : base()
        {
            DatabasePath = databasePath;
        }

        public string DatabasePath { get; }


        public DbSet<Models.Password> Passwords { get; set; }
        public DbSet<Models.PasswordTag> PasswordTagLinks { get; set; }
        public DbSet<Models.Tag> Tags { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.PasswordTag>()
                        .HasKey(pt => new { pt.PasswordId, pt.TagId });
        }

    }
}
