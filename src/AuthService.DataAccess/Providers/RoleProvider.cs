using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Providers
{
    /// <summary>
    /// Role provider implmentation.
    /// </summary>
    /// <seealso cref="BusinessLogic.Contracts.DataAccess.Providers.IRoleProvider" />
    public class RoleProvider : IRoleProvider
    {
        private readonly DbSet<Role> _entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleProvider"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public RoleProvider(DbSet<Role> entities)
        {
            _entities = entities;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// List of roles
        /// </returns>
        public Task<List<Role>> GetAll(CancellationToken token)
        {
            return _entities
                .AsNoTracking()
                .ToListAsync(token);
        }
    }
}
