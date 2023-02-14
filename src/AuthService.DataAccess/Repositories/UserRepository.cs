using AuthService.BusinessLogic.Contracts.Repositories;
using AuthService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repositories
{
    /// <summary>
    /// User repository implementation.
    /// </summary>
    /// <seealso cref="AuthService.BusinessLogic.Contracts.Repositories.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _entities;

        private readonly bool _useHiLoGenerators;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public UserRepository(DbSet<User> entities)
        {
            _entities = entities;
        }

        public Task Add(User entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            if (_useHiLoGenerators)
            {
                await _entities.AddAsync(entity, token);
            }
            else
            {
                _entities.Add(entity);
            }
        }

        public Task AddRange(IEnumerable<User> entities, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task Remove(User entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task Update(User entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRange(IEnumerable<User> entities, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
