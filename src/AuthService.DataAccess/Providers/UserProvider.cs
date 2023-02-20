using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Providers
{
    /// <summary>
    /// User provider implementation
    /// </summary>
    /// <seealso cref="BusinessLogic.Contracts.DataAccess.Providers.IUserProvider" />
    public class UserProvider : IUserProvider
    {
        private readonly DbSet<User> _entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProvider"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public UserProvider(DbSet<User> entities)
        {
            _entities = entities;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <param name="token">The token.</param>
        /// <returns>User?</returns>
        public Task<User?> GetById(Guid userId, CancellationToken token)
        {
            return _entities
                .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == userId, token);
        }

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="token">The token.</param>
        /// <returns>User?</returns>
        public Task<User?> GetUserByEmail(string email, CancellationToken token)
        {
            return string.IsNullOrEmpty(email)
                ? Task.FromResult(default(User?))
                : _entities
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Email == email, token);
        }
    }
}
