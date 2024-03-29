﻿using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Providers
{
    /// <summary>
    /// User roles provider.
    /// </summary>
    /// <seealso cref="BusinessLogic.Contracts.DataAccess.Providers.IUserRolesProvider" />
    public class UserRolesProvider : IUserRolesProvider
    {
        private readonly DbSet<UserRole> _entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRolesProvider"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public UserRolesProvider(DbSet<UserRole> entities)
        {
            _entities = entities;
        }

        /// <inheritdoc/>
        public Task<List<Role?>> GetUserRoles(Guid userId, CancellationToken token)
        {
            return _entities
                   .AsNoTracking()
                   .Where(x => x.UserId == userId)
                   .Select(x => x.Role)
                   .ToListAsync(token);
        }
    }
}
