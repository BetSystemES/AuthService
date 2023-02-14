using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuthService.DataAccess
{
    /// <summary>
    /// Auction service context factory
    /// </summary>
    public class AuthServiceContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of <typeparamref name="TContext" />.
        /// </returns>
        public AuthDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AuthDb;User Id=postgres;Password=123");

            return new AuthDbContext(optionsBuilder.Options);
        }
    }
}
