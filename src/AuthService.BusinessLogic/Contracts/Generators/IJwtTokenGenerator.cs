using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.Generators
{
    public interface IJwtTokenGenerator
    {
        TToken Generate<TToken>(User user, DateTime issuedAtUtc, CancellationToken cancellationToken) where TToken : JwtToken;
    }
}
