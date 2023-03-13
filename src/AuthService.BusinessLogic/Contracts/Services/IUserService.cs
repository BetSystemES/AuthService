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
        /// <param name="model">The model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// User simplified model
        /// </returns>
        Task<UserSimpleModel> CreateUser(CreateUserModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task Remove(Guid userId, CancellationToken cancellationToken);
    }
}
