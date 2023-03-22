using Microsoft.Extensions.Logging;
using FizzWare.NBuilder;
using Moq;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models;
using AuthService.UnitTests.Infrastructure;

namespace AuthService.UnitTests.Services
{
    [Trait(Constants.Category, Constants.UnitTest)]
    public class AuthServiceTests
    {
        private readonly Mock<IRefreshTokenProvider> _refreshTokenProvider;
        private readonly Mock<IUserProvider> _userProvider;
        private readonly Mock<ITokenGenerator> _tokenGenerator;
        private readonly Mock<IRefreshTokenRepository> _refreshTokenRepository;
        private readonly Mock<IHashProvider> _hashProvider;
        private readonly Mock<ILogger<BusinessLogic.Services.AuthService>> _logger;

        private readonly BusinessLogic.Services.AuthService _target;

        public AuthServiceTests()
        {
            _hashProvider = new();
            _refreshTokenProvider = new();
            _userProvider = new();
            _tokenGenerator = new();
            _refreshTokenRepository = new();
            _logger = new();

            _target = new(_refreshTokenProvider.Object,
                                    _userProvider.Object,
                                    _tokenGenerator.Object,
                                    _refreshTokenRepository.Object,
                                    _hashProvider.Object,
                                    _logger.Object);
        }

        [Fact]
        public async Task AuthenticateTest()
        {
            // Arrange
            var userToken = Builder<UserRefreshToken>
                .CreateNew()
                .Build();

            var user = Builder<User>
                .CreateNew()
                .Build();

            var token = Builder<Token>
                .CreateNew()
                .Build();

            _refreshTokenProvider.Setup(x => x.GetToken(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userToken)
                .Verifiable();

            _userProvider.Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();

            _refreshTokenRepository.Setup(x => x.Remove(userToken, It.IsAny<CancellationToken>()))
                .Verifiable();

            _tokenGenerator.Setup(x => x.GenerateTokenAsync(user, It.IsAny<CancellationToken>()))
                .ReturnsAsync(token)
                .Verifiable();

            // Act
            var result = await _target.Authenticate(It.IsAny<string>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.Equal(token, result);

            _refreshTokenProvider.Verify(x => x.GetToken(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            _userProvider.Verify(x => x.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _refreshTokenRepository.Verify(x => x.Remove(userToken, It.IsAny<CancellationToken>()), Times.Once);
            _tokenGenerator.Verify(x => x.GenerateTokenAsync(user, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AuthenticateApplicationExceptionTest(bool isUserTokenExists)
        {
            // Arrange
            var userToken = isUserTokenExists ? Builder<UserRefreshToken>
                .CreateNew()
                .Build() : default;

            _refreshTokenProvider.Setup(x => x.GetToken(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userToken)
                .Verifiable();

            _userProvider.Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(User))
                .Verifiable();

            // Act
            // Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await _target.Authenticate(It.IsAny<string>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task AuthenticateOverrideTest()
        {
            // Arrange
            var user = Builder<User>
                .CreateNew()
                .Build();

            var token = Builder<Token>
                .CreateNew()
                .Build();

            _userProvider.Setup(x => x.GetUserByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();

            _hashProvider.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true)
                .Verifiable();

            _refreshTokenProvider.Setup(x => x.GetByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(UserRefreshToken))
                .Verifiable();

            _tokenGenerator.Setup(x => x.GenerateTokenAsync(user, It.IsAny<CancellationToken>()))
                .ReturnsAsync(token)
                .Verifiable();

            // Act
            var result = await _target.Authenticate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.Equal(token, result);

            _refreshTokenProvider.Verify(x => x.GetByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _userProvider.Verify(x => x.GetUserByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            _hashProvider.Verify(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _tokenGenerator.Verify(x => x.GenerateTokenAsync(user, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AuthenticateOverrideApplicationExceptionTest(bool isUserExists)
        {
            // Arrange
            var user = isUserExists ? Builder<User>
                .CreateNew()
                .Build() : default;

            _userProvider.Setup(x => x.GetUserByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();

            _hashProvider.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false)
                .Verifiable();

            // Act
            // Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await _target.Authenticate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
        }
    }
}
