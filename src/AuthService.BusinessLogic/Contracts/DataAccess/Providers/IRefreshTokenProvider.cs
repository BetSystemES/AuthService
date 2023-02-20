using AuthService.BusinessLogic.Models;

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
    }
}
