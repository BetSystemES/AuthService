using AuthService.BusinessLogic.Entities;
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
        /// Created user instance.
        /// </returns>
        Task<User> CreateUser(CreateUserModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task</returns>
        Task Remove(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="email">The email.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Task
        /// </returns>
        Task Remove(Guid userId, string email, CancellationToken cancellationToken);
    }
}
