using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Contracts.Services;
using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRefreshTokenProvider _refreshTokenProvider;
        private readonly IUserProvider _userProvider;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHashProvider _hashProvider;

        public AuthService
        (
            IRefreshTokenProvider refreshTokenProvider,
            IUserProvider userProvider,
            ITokenGenerator tokenGenerator,
            IRefreshTokenRepository refreshTokenRepository,
            IHashProvider hashProvider
        )
        {
            _refreshTokenProvider = refreshTokenProvider;
            _userProvider = userProvider;
            _tokenGenerator = tokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _hashProvider = hashProvider;
        }

        public async Task<Token> Authenticate(string refreshToken, CancellationToken cancellationToken)
        {
            var userToken = await _refreshTokenProvider.GetToken(refreshToken, cancellationToken);
            if (userToken == null)
            {
                throw new ApplicationException("Refresh token not found or expired.");
            }

            var user = await _userProvider.GetById(userToken.UserId, cancellationToken);
            if (user == null)
            {
                throw new ApplicationException("Associated user not found.");
            }

            await _refreshTokenRepository.Remove(userToken, cancellationToken);
            var token = await _tokenGenerator.GenerateTokenAsync(user, cancellationToken);

            return token;
        }

        public async Task<Token> Authenticate(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _userProvider.GetUserByEmail(email, cancellationToken);
            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            if (!_hashProvider.Verify(password, user.PasswordHash!))
            {
                throw new ApplicationException("Invalid password.");
            }

            // TODO: if refresh exist = delete
            
            var token = await _tokenGenerator.GenerateTokenAsync(user, cancellationToken);

            return token;
        }
    }
}
