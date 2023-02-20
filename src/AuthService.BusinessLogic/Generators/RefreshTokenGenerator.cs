using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Worker;
using AuthService.BusinessLogic.Models;
using Microsoft.Extensions.Options;

namespace AuthService.BusinessLogic.Generators
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        private readonly JWTConfig _jWTConfig;
        private readonly IJWTWorker _jWTWorker;

        public RefreshTokenGenerator(IOptions<JWTConfig> jWTConfig, IJWTWorker jWTWorker)
        {
            _jWTConfig = jWTConfig.Value;
            _jWTWorker = jWTWorker;
        }

        public RefreshToken Generate(User user, DateTime issuedAtUtc, CancellationToken cancellationToken)
        {
            var expiresAtUtc = issuedAtUtc.AddMinutes(_jWTConfig.RefreshTokenLifetimeInMinutes);

            var token = _jWTWorker.GenerageToken(
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
