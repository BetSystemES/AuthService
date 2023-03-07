using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repositories
{
    /// <summary>
    /// Refresh token repository implementation.
    /// </summary>
    /// <seealso cref="AuthService.DataAccess.Repositories.SqlRepository&lt;UserRefreshToken&gt;" />
    /// <seealso cref="BusinessLogic.Contracts.DataAccess.Repositories.IRefreshTokenRepository" />
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
