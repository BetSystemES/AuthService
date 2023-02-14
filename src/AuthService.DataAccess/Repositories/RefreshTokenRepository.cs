using AuthService.BusinessLogic.Contracts.Repositories;
using AuthService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repositories
{
    /// <summary>
    /// Refresh token repository implementation.
    /// </summary>
    /// <seealso cref="AuthService.DataAccess.Repositories.SqlRepository&lt;AuthService.BusinessLogic.Models.UserRefreshToken&gt;" />
    /// <seealso cref="AuthService.BusinessLogic.Contracts.Repositories.IRefreshTokenRepository" />
    public class RefreshTokenRepository : SqlRepository<UserRefreshToken>, IRefreshTokenRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenRepository"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public RefreshTokenRepository(DbSet<UserRefreshToken> provider) : base(provider)
        {
        }
    }
}
