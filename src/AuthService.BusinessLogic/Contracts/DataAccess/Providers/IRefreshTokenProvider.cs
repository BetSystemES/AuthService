using AuthService.BusinessLogic.Entities;

namespace AuthService.BusinessLogic.Contracts.DataAccess.Providers
{
    /// <summary>
    /// Refresh token provider
    /// </summary>
    public interface IRefreshTokenProvider
    {
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="token">The token.</param>
        /// <returns>UserRefreshToken</returns>
        Task<UserRefreshToken?> GetToken(string refreshToken, CancellationToken token);

        /// <summary>
        /// Gets the by user identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>User refresh token.</returns>
        Task<UserRefreshToken?> GetByUserId(Guid id, CancellationToken cancellationToken);
    }
}
