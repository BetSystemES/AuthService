using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.Generators
{
    public interface IJwtTokenGenerator
    {
        JwtToken Generate(User user, DateTime issuedAtUtc, CancellationToken cancellationToken);
    }
}
