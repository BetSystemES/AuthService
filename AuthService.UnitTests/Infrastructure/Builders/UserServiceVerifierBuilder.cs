using FizzWare.NBuilder;
using Moq;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.DataAccess;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Services;
using AuthService.UnitTests.Infrastructure.Verifiers;
using AuthService.BusinessLogic.Models;
using User = AuthService.BusinessLogic.Entities.User;

namespace AuthService.UnitTests.Infrastructure.Builders
{
    public class UserServiceVerifierBuilder
    {
        private readonly Mock<IUserProvider> _userProvider;
        private readonly Mock<IHashProvider> _hashProvider;
        private readonly Mock<IDateTimeProvider> _dateTimeProvider;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IDataContext> _dataContext;

        private CreateUserModel _model;
        private User _user;

        private readonly UserService _userService;

        public UserServiceVerifierBuilder()
        {
            _userProvider = new();
            _hashProvider = new();
            _dateTimeProvider = new();
            _userRepository = new();
            _dataContext = new();

            _userService = new(
                _userProvider.Object,
                _hashProvider.Object,
                _dateTimeProvider.Object,
                _userRepository.Object,
                _dataContext.Object);
        }

        public UserServiceVerifierBuilder AddUserServiceCreateUserModel()
        {
            var userRoles = Builder<Guid>
                .CreateListOfSize(3)
                .All()
                .With(_ => Guid.NewGuid())
                .Build();

            _model = Builder<CreateUserModel>
                .CreateNew()
                .With(x => x.RoleIds = userRoles.ToList())
                .Build();

            return this;
        }

        public UserServiceVerifierBuilder AddUserServiceUser()
        {
            _user = Builder<User>
                .CreateNew()
                .Build();

            return this;
        }

        public UserServiceTestsVerifier Build()
        {
            return new UserServiceTestsVerifier(
                _userProvider,
                _hashProvider,
                _dateTimeProvider,
                _userRepository,
                _dataContext,
                _userService,
                _model);
        }

        public UserServiceVerifierBuilder SetupUserServiceHashProvider()
        {
            _hashProvider.Setup(x => x.Hash(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();

            return this;
        }

        public UserServiceVerifierBuilder SetupUserServiceUserRepositoryAdd()
        {
            _userRepository.Setup(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            return this;
        }

        public UserServiceVerifierBuilder SetupUserServiceUserRepositoryRemove()
        {
            _userRepository.Setup(x => x.Remove(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            return this;
        }

        public UserServiceVerifierBuilder SetupUserServiceDataContextSaveChanges()
        {
            _dataContext.Setup(x => x.SaveChanges(It.IsAny<CancellationToken>()))
                .Verifiable();

            return this;
        }

        public UserServiceVerifierBuilder SetupUserServiceUserProviderGetById()
        {
            _userProvider.Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_user)
                .Verifiable();

            return this;
        }
    }
}
