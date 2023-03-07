using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Providers
{
    /// <summary>
    /// Refresh token provider.
    /// </summary>
    /// <seealso cref="BusinessLogic.Contracts.DataAccess.Providers.IRefreshTokenProvider" />
    public class RefreshTokenProvider : IRefreshTokenProvider
    {
        private readonly DbSet<UserRefreshToken> _entities;
        private readonly IDateTimeProvider _dateTimeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenProvider"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        public RefreshTokenProvider(DbSet<UserRefreshToken> entities, IDateTimeProvider dateTimeProvider)
        {
            _entities = entities;
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// UserRefreshToken
        /// </returns>
        public Task<UserRefreshToken?> GetToken(string refreshToken, CancellationToken token)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Task.FromResult(default(UserRefreshToken));
            }

            var now = _dateTimeProvider.NowUtc;
            return _entities
                    .AsNoTracking()
                    .TagWith("Get active refresh token by value")
                    .FirstOrDefaultAsync(x => x.Token == refreshToken &&
                                            x.ExpiresAtUtc > now &&
                                            x.IssuedAtUtc < now, token);
        }
    }
}
