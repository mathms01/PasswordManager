using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PasswordsManager.DataAccess
{
    public class PasswordsDbContextFactory : IDesignTimeDbContextFactory<PasswordsDbContext>
    {
        public PasswordsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PasswordsDbContext>();
            optionsBuilder.UseSqlite("Data Source=passwords.db");

            return new PasswordsDbContext(optionsBuilder.Options);
        }
    }
}
