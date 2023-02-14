using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.Providers
{
    /// <summary>
    /// Users provider
    /// </summary>
    public interface IUserProvider
    {
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>User</returns>
        Task<User?> GetById(Guid userId, CancellationToken token);

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="token">The token.</param>
        /// <returns>User</returns>
        Task<User?> GetUserByEmail(string email, CancellationToken token);
    }
}
