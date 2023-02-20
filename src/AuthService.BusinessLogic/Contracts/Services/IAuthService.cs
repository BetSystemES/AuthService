using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.Services
{
    /// <summary>
    /// Auth Service
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates the specified refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Token</returns>
        Task<Token> Authenticate(string refreshToken, CancellationToken cancellationToken);

        /// <summary>
        /// Authenticates the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Token</returns>
        Task<Token> Authenticate(string email, string password, CancellationToken cancellationToken);
    }
}
