using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly DbSet<UserRole> _entities;

        public UserRoleRepository(DbSet<UserRole> entities)
        {
            _entities = entities;
        }
        public async Task AddToUser(IEnumerable<UserRole> items)
        {
            await _entities.AddRangeAsync(items);
        }
    }
}
