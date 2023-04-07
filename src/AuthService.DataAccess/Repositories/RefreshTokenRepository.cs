using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Entities;

namespace AuthService.DataAccess.Repositories
{
    /// <summary>
    /// Refresh token repository implementation.
    /// </summary>
    /// <seealso cref="SqlRepository&lt;UserRefreshToken&gt;" />
    /// <seealso cref="IRefreshTokenRepository" />
    public class RefreshTokenRepository : SqlRepository<UserRefreshToken>, IRefreshTokenRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenRepository"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public RefreshTokenRepository(AuthDbContext context) : base(context)
        {
        }
    }
}
