using AuthService.BusinessLogic.Entities;

namespace AuthService.BusinessLogic.Contracts.Services
{
    /// <summary>
    /// Role service contract.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The range of all roles.</returns>
        Task<IEnumerable<Role>> GetAll(CancellationToken cancellationToken);
    }
}
