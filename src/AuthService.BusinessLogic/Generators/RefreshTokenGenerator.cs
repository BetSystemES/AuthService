using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Worker;
using AuthService.BusinessLogic.Models;
using Microsoft.Extensions.Options;

namespace AuthService.BusinessLogic.Generators
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        // TODO: typo in _jWTConfig. Should be _jWtConfig
        // TODO: typo in _jWTWorker. Should be _jWtWorker
        private readonly JWTConfig _jWTConfig;
        private readonly IJWTWorker _jWTWorker;

        // TODO: typo in jWTConfig. Should be jWtConfig
        // TODO: typo in _jWTWorker. Should be _jWtWorker
        public RefreshTokenGenerator(IOptions<JWTConfig> jWTConfig, IJWTWorker jWTWorker)
        {
            _jWTConfig = jWTConfig.Value;
            _jWTWorker = jWTWorker;
        }

        public RefreshToken Generate(User user, DateTime issuedAtUtc, CancellationToken cancellationToken)
        {
            var expiresAtUtc = issuedAtUtc.AddMinutes(_jWTConfig.RefreshTokenLifetimeInMinutes);

            // TODO: typo GenerageToken. Should be GenerateToken
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
