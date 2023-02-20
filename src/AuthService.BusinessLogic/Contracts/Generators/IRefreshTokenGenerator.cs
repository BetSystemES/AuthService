using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.Generators
{
    public interface IRefreshTokenGenerator
    {
        RefreshToken Generate(User user, DateTime issuedAtUtc, CancellationToken cancellationToken)
    }
}
