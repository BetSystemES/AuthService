using Moq;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models;

namespace AuthService.UnitTests.Infrastructure.Verifiers
{
    public class AuthServiceTestsVerifier
    {
        private readonly Mock<IRefreshTokenProvider> _refreshTokenProvider;
        private readonly Mock<IUserProvider> _userProvider;
        private readonly Mock<ITokenGenerator> _tokenGenerator;
        private readonly Mock<IRefreshTokenRepository> _refreshTokenRepository;
        private readonly Mock<IHashProvider> _hashProvider;

        public readonly Token? ExpectedResult;

        public readonly BusinessLogic.Services.AuthService AuthService;

        public AuthServiceTestsVerifier(
            Mock<IHashProvider> hashProvider,
            Mock<IRefreshTokenProvider> refreshTokenProvider,
            Mock<IUserProvider> userProvider,
            Mock<ITokenGenerator> tokenGenerator,
            Mock<IRefreshTokenRepository> refreshTokenRepository,
            BusinessLogic.Services.AuthService authService,
            Token expectedResult)
        {
            _hashProvider = hashProvider;
            _refreshTokenProvider = refreshTokenProvider;
            _userProvider = userProvider;
            _tokenGenerator = tokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            AuthService = authService;
            ExpectedResult = expectedResult;
        }

        public AuthServiceTestsVerifier VerifyRefreshTokenProviderGetToken()
        {
            _refreshTokenProvider.Verify(x => x.GetToken(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public AuthServiceTestsVerifier VerifyRefreshTokenProviderGetByUserId()
        {
            _refreshTokenProvider.Verify(x => x.GetByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public AuthServiceTestsVerifier VerifyUserProviderGetById()
        {
            _userProvider.Verify(x => x.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public AuthServiceTestsVerifier VerifyUserProviderGetUserByEmail()
        {
            _userProvider.Verify(x => x.GetUserByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public AuthServiceTestsVerifier VerifyRefreshTokenRepositoryRemove()
        {
            _refreshTokenRepository.Verify(x => x.Remove(It.IsAny<UserRefreshToken>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public AuthServiceTestsVerifier VerifyTokenGeneratorGenerateTokenAsync()
        {
            _tokenGenerator.Verify(x => x.GenerateTokenAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public AuthServiceTestsVerifier VerifyHashProvider()
        {
            _hashProvider.Verify(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            return this;
        }
    }
}
