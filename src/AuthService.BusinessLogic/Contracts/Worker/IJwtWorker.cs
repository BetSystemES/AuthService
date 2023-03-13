using AuthService.BusinessLogic.Entities;

namespace AuthService.BusinessLogic.Contracts.Worker
{
    /// <summary>
    /// Jwt worker contract.
    /// </summary>
    public interface IJwtWorker
    {
        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="issuedAtUtc">The issued at UTC.</param>
        /// <param name="expiresDelayInMinutes">The expires delay in minutes.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>JWT.</returns>
        string? GenerateToken(User user,
            DateTime issuedAtUtc,
            TimeSpan expiresDelayInMinutes,
            IEnumerable<Role> roles,
            CancellationToken cancellationToken);

        /// <summary>
        /// Validates jwt.
        /// </summary>
        /// <param name="token">JWT</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User name from token.</returns>
        Guid? ValidateToken(string token, CancellationToken cancellationToken);
    }
}
