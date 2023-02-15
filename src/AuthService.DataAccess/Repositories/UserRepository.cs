using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repositories
{
    /// <summary>
    /// User repository implementation.
    /// </summary>
    /// <seealso cref="AuthService.DataAccess.Repositories.SqlRepository&lt;AuthService.BusinessLogic.Models.User&gt;" />
    /// <seealso cref="BusinessLogic.Contracts.DataAccess.Repositories.IUserRepository" />
    public class UserRepository : SqlRepository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public UserRepository(DbSet<User> provider) : base(provider)
        {
        }
    }
}
