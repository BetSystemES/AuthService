using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Worker;
using AuthService.BusinessLogic.Models;
using Microsoft.Extensions.Options;

namespace AuthService.BusinessLogic.Generators
{
    public class JWTTokenGenerator : IJwtTokenGenerator
    {
        private readonly JWTConfig _jWTConfig;
        private readonly IJWTWorker _jWTWorker;

        public JWTTokenGenerator(IOptions<JWTConfig> jWTConfig, IJWTWorker jWTWorker)
        {
            _jWTConfig = jWTConfig.Value;
            _jWTWorker = jWTWorker;
        }

        public JwtToken Generate(User user, DateTime issuedAtUtc, CancellationToken cancellationToken)
        {
            var expiresAtUtc = issuedAtUtc.AddMinutes(_jWTConfig.TokenLifetimeInMinutes);

            var token = _jWTWorker.GenerageToken(
                user,
                issuedAtUtc,
                TimeSpan.FromMinutes(_jWTConfig.TokenLifetimeInMinutes),
                cancellationToken);

            var tokenModel = new JwtToken()
            {
                ExpiresAtUtc = expiresAtUtc,
                IssuedAtUtc = issuedAtUtc,
                Token = token
            };

            return tokenModel;
        }
    }
}
