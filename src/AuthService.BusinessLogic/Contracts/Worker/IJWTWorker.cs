using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.Worker
{
    // TODO: typo in IJWTWorker. Should be IJwtWorker
    /// <summary>
    /// Jwt worker contract.
    /// </summary>
    public interface IJWTWorker
    {
        // TODO: typo in ExpiresDelayInMinutes. Should be expiresDelayInMinutes
        // TODO: typo in GenerageToken. Should be GenerateToken
        /// <summary>
        /// Generages the token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="ExpiresDelayInMinutes">The expires delay in minutes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>JWT</returns>
        string? GenerageToken(User user, DateTime issuedAtUtc, TimeSpan ExpiresDelayInMinutes, CancellationToken cancellationToken);

        /// <summary>
        /// Validates jwt.
        /// </summary>
        /// <param name="token">JWT.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User name from token.</returns>
        Guid? ValidateToken(string token, CancellationToken cancellationToken);
    }
}
