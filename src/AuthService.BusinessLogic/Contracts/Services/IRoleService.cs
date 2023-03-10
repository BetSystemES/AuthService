using AuthService.BusinessLogic.Entities;

namespace AuthService.BusinessLogic.Contracts.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAll(CancellationToken cancellationToken);
    }
}
