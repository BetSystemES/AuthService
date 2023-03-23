using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Extensions;
using AuthService.BusinessLogic.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Providers
{
    /// <summary>
    /// Role provider implementation.
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

        /// <inheritdoc/>
        public Task<List<Role>> GetAll(CancellationToken token)
        {
            return _entities
                .AsNoTracking()
                .ToListAsync(token);
        }

        public Task<List<Role>> GetDefault(CancellationToken token)
        {
            return _entities
                .AsNoTracking()
                .Where(x=> string.Equals(x.Name, AuthRole.User.GetDescription()))
                .ToListAsync(token);
        }
    }
}
