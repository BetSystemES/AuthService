using FizzWare.NBuilder;
using Moq;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.DataAccess;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models;
using AuthService.BusinessLogic.Services;
using AuthService.UnitTests.Infrastructure;

namespace AuthService.UnitTests.Services
{
    [Trait(Constants.Category, Constants.UnitTest)]
    public class UserServiceTests
    {
        private readonly Mock<IUserProvider> _userProvider;
        private readonly Mock<IHashProvider> _hashProvider;
        private readonly Mock<IDateTimeProvider> _dateTimeProvider;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IDataContext> _dataContext;

        private readonly UserService _target;

        public UserServiceTests()
        {
            _userProvider = new();
            _hashProvider = new();
            _dateTimeProvider = new();
            _userRepository = new();
            _dataContext = new();

            _target = new(_userProvider.Object,
                                    _hashProvider.Object,
                                    _dateTimeProvider.Object,
                                    _userRepository.Object,
                                    _dataContext.Object);
        }

        [Fact]
        public async Task CreateUserTest()
        {
            // Arrange
            var userRoles = Builder<Guid>
                .CreateListOfSize(3)
                .All()
                .With(_ => Guid.NewGuid())
                .Build();

            var model = Builder<CreateUserModel>
                .CreateNew()
                .With(x => x.RoleIds = userRoles.ToList())
                .Build();

            _hashProvider.Setup(x => x.Hash(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();

            _userRepository.Setup(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            _dataContext.Setup(x => x.SaveChanges(It.IsAny<CancellationToken>()))
                .Verifiable();

            // Act
            await _target.CreateUser(model, It.IsAny<CancellationToken>());

            // Assert
            _hashProvider.Verify(x => x.Hash(It.IsAny<string>()), Times.Once());
            _userRepository.Verify(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            _dataContext.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetUserSimpleModelTest()
        {
            // Arrange
            var user = Builder<User>
                .CreateNew()
                .Build();

            _userProvider.Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();

            // Act
            await _target.GetUserSimpleModel(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            // Assert
            _userProvider.Verify(x => x.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void GetUserSimpleModelApplicationExceptionTest()
        {
            // Arrange
            _userProvider.Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(User))
                .Verifiable();

            // Act
            // Assert
            // TODO: please use library FluentAssertion
            Assert.ThrowsAsync<ApplicationException>(async () => await _target.GetUserSimpleModel(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task RemoveTest()
        {
            // Arrange
            _userRepository.Setup(x => x.Remove(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            _dataContext.Setup(x => x.SaveChanges(It.IsAny<CancellationToken>()))
                .Verifiable();

            // Act
            await _target.Remove(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            // Assert
            _userRepository.Verify(x => x.Remove(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            _dataContext.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
