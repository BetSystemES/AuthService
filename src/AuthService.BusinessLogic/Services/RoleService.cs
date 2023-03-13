using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.Services;
using AuthService.BusinessLogic.Entities;

namespace AuthService.BusinessLogic.Services
{
    /// <summary>
    /// Role service implementation.
    /// </summary>
    /// <seealso cref="AuthService.BusinessLogic.Contracts.Services.IRoleService" />
    public class RoleService : IRoleService
    {
        private readonly IRoleProvider _roleProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="roleProvider">The role provider.</param>
        public RoleService(IRoleProvider roleProvider)
        {
            _roleProvider = roleProvider;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Role>> GetAll(CancellationToken cancellationToken)
        {
            return await _roleProvider.GetAll(cancellationToken);
        }
    }
}
