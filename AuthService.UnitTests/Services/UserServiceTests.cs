using Moq;
using FluentAssertions;
using AuthService.UnitTests.Infrastructure;
using AuthService.UnitTests.Infrastructure.Builders;

namespace AuthService.UnitTests.Services
{
    [Trait(Constants.Category, Constants.UnitTest)]
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUserTest()
        {
            // Arrange
            var verifier = new UserServiceVerifierBuilder()
                .AddUserServiceCreateUserModel()
                .SetupUserServiceHashProvider()
                .SetupUserServiceUserRepositoryAdd()
                .SetupUserServiceDataContextSaveChanges()
                .Build();

            // Act
            await verifier.UserService.CreateUser(verifier.Model, It.IsAny<CancellationToken>());

            // Assert
            verifier
                .VerifyHashProvider()
                .VerifyUserRepositoryAdd()
                .VerifyDataContextSaveChanges();
        }

        [Fact]
        public async Task GetUserSimpleModelTest()
        {
            // Arrange
            var verifier = new UserServiceVerifierBuilder()
                .AddUserServiceUser()
                .SetupUserServiceUserProviderGetById()
                .Build();

            // Act
            await verifier.UserService.GetUserSimpleModel(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            // Assert
            verifier.VerifyUserProviderGetById();
        }

        [Fact]
        public void GetUserSimpleModelApplicationExceptionTest()
        {
            // Arrange
            var verifier = new UserServiceVerifierBuilder()
                .SetupUserServiceUserProviderGetById()
                .Build();

            // Act
            Func<Task> result = async () => await verifier.UserService.GetUserSimpleModel(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            // Assert
            result.Should().ThrowAsync<ApplicationException>();

            verifier.VerifyUserProviderGetById();
        }

        [Fact]
        public async Task RemoveTest()
        {
            // Arrange
            var verifier = new UserServiceVerifierBuilder()
                .SetupUserServiceUserRepositoryRemove()
                .SetupUserServiceDataContextSaveChanges()
                .Build();

            // Act
            await verifier.UserService.Remove(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            // Assert
            verifier
                .VerifyUserRepositoryRemove()
                .VerifyDataContextSaveChanges();
        }
    }
}
