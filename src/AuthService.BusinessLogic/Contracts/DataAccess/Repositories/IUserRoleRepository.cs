using AuthService.BusinessLogic.Models.Enums;

namespace AuthService.BusinessLogic.Contracts.DataAccess.Repositories
{
    /// <summary>
    /// Role repository implementation.
    /// </summary>
    public interface IUserRoleRepository
    {
        /// <summary>
        /// Adds to user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        Task AddToUser(Guid userId, Guid roleId);
    }
}
