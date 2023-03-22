using Moq;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.DataAccess;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models;
using AuthService.BusinessLogic.Services;

namespace AuthService.UnitTests.Infrastructure.Verifiers
{
    public class UserServiceTestsVerifier
    {
        private readonly Mock<IUserProvider> _userProvider;
        private readonly Mock<IHashProvider> _hashProvider;
        private readonly Mock<IDateTimeProvider> _dateTimeProvider;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IDataContext> _dataContext;

        public readonly CreateUserModel Model;

        public readonly UserService UserService;

        public UserServiceTestsVerifier(
            Mock<IUserProvider> userProvider,
            Mock<IHashProvider> hashProvider,
            Mock<IDateTimeProvider> dateTimeProvider,
            Mock<IUserRepository> userRepository,
            Mock<IDataContext> dataContext,
            UserService userService,
            CreateUserModel model)
        {
            _userProvider = userProvider;
            _hashProvider = hashProvider;
            _dateTimeProvider = dateTimeProvider;
            _userRepository = userRepository;
            _dataContext = dataContext;
            UserService = userService;
            Model = model;
        }

        public UserServiceTestsVerifier VerifyHashProvider()
        {
            _hashProvider.Verify(x => x.Hash(It.IsAny<string>()), Times.Once());

            return this;
        }

        public UserServiceTestsVerifier VerifyUserRepositoryAdd()
        {
            _userRepository.Verify(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public UserServiceTestsVerifier VerifyUserRepositoryRemove()
        {
            _userRepository.Verify(x => x.Remove(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public UserServiceTestsVerifier VerifyDataContextSaveChanges()
        {
            _dataContext.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public UserServiceTestsVerifier VerifyUserProviderGetById()
        {
            _userProvider.Verify(x => x.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }
    }
}
