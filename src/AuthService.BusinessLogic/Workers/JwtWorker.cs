﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.BusinessLogic.Constants;
using AuthService.BusinessLogic.Contracts.Worker;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models.AppSettings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.BusinessLogic.Workers
{
    /// <summary>
    /// Jwt worker implementation.
    /// </summary>
    /// <seealso cref="AuthService.BusinessLogic.Contracts.Worker.IJwtWorker" />
    public class JwtWorker : IJwtWorker
    {
        private readonly JwtConfig _jwtConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtWorker"/> class.
        /// </summary>
        /// <param name="jwtConfig">The jwt configuration.</param>
        public JwtWorker(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        /// <inheritdoc/>
        public string? GenerateToken(User user,
            DateTime issuedAtUtc,
            TimeSpan expiresDelayInMinutes,
            IEnumerable<Role> roles,
            CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtAuthConstants.DefaultIdFieldName, user.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(",", roles.Select(x => x.Name)))
                }),
                Issuer = _jwtConfig.Issuer,
                Expires = issuedAtUtc.Add(expiresDelayInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        /// <inheritdoc/>
        public Guid? ValidateToken(string token, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret!);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtConfig.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == JwtAuthConstants.DefaultIdFieldName).Value);

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
