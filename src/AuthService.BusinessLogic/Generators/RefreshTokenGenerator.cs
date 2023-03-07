using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Worker;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models;
using AuthService.BusinessLogic.Models.AppSettings;
using Microsoft.Extensions.Options;

namespace AuthService.BusinessLogic.Generators
{
    // TODO: class can be deleted because we have already JwtTokenGenerator class
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        private readonly JwtConfig _jWTConfig;
        private readonly IJwtWorker _jWTWorker;

        public RefreshTokenGenerator(IOptions<JwtConfig> jWTConfig, IJwtWorker jWTWorker)
        {
            _jWTConfig = jWTConfig.Value;
            _jWTWorker = jWTWorker;
        }

        public RefreshToken Generate(User user, DateTime issuedAtUtc, CancellationToken cancellationToken)
        {
            var expiresAtUtc = issuedAtUtc.AddMinutes(_jWTConfig.RefreshTokenLifetimeInMinutes);

            var token = _jWTWorker.GenerateToken(
                user,
                issuedAtUtc,
                TimeSpan.FromMinutes(_jWTConfig.RefreshTokenLifetimeInMinutes),
                cancellationToken);

            var tokenModel = new RefreshToken()
            {
                ExpiresAtUtc = expiresAtUtc,
                IssuedAtUtc = issuedAtUtc,
                Token = token
            };

            return tokenModel;
        }
    }
}
