using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.Services;
using AuthService.BusinessLogic.Entities;

namespace AuthService.BusinessLogic.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleProvider _roleProvider;

        public RoleService(IRoleProvider roleProvider)
        {
            _roleProvider = roleProvider;
        }

        public async Task<IEnumerable<Role>> GetAll(CancellationToken cancellationToken)
        {
            return await _roleProvider.GetAll(cancellationToken);
        }
    }
}
