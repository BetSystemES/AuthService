using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.Services
{
    /// <summary>
    /// User service contract.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets the user simple model.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>User simplified model</returns>
        Task<UserSimpleModel> GetUserSimpleModel(Guid userId, CancellationToken token);

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="claims">The claims.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>User simplified model</returns>
        Task<UserSimpleModel> CreateUser(CreateUserModel model, CancellationToken cancellationToken);
    }
}
