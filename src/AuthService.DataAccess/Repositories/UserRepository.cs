using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Entities;

namespace AuthService.DataAccess.Repositories
{
    /// <summary>
    /// User repository implementation.
    /// </summary>
    /// <seealso cref="SqlRepository&lt;User&gt;" />
    /// <seealso cref="IUserRepository" />
    public class UserRepository : SqlRepository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public UserRepository(AuthDbContext context) : base(context)
        {
        }
    }
}
