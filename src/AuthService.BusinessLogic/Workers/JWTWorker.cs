using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.BusinessLogic.Contracts.Worker;
using AuthService.BusinessLogic.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.BusinessLogic.Workers
{
    /// <summary>
    /// Jwt worker implementation.
    /// </summary>
    /// <seealso cref="AuthService.BusinessLogic.Contracts.Worker.IJWTWorker" />
    public class JWTWorker : IJWTWorker
    {
        private readonly JWTConfig _jWTConfig;

        private static readonly string _defaultIdFieldName = "id";

        /// <summary>
        /// Initializes a new instance of the <see cref="JWTWorker"/> class.
        /// </summary>
        /// <param name="jWTConfig">The jwt configuration.</param>
        public JWTWorker(IOptions<JWTConfig> jWTConfig)
        {
            _jWTConfig = jWTConfig.Value;
        }

        public string? GenerageToken(User user, DateTime issuedAtUtc, TimeSpan ExpiresDelayInMinutes, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Secret!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(_defaultIdFieldName, user.Id.ToString()) }),
                Expires = issuedAtUtc.Add(ExpiresDelayInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// return user id from JWT token if validation successful;
        /// return null if validation fails;
        /// </returns>
        public Guid? ValidateToken(string token, CancellationToken cancellationToken)
        {
            if (token == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jWTConfig.Secret!);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == _defaultIdFieldName).Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
