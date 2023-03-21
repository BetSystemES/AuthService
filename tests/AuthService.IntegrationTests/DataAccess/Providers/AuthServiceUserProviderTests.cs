using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models;
using FizzWare.NBuilder;
using FluentAssertions;

namespace AuthService.IntegrationTests.DataAccess.Providers
{
    public class AuthServiceUserProviderTests : BaseTestClass
    {
        public AuthServiceUserProviderTests(GrpcAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetById_Should_Return_Result()
        {
            //Arrange
            var user = await ArrangeUser();

            //Act
            var actualResult = await UserProvider.GetById(user.Id, CancellationToken);

            // Assert
            actualResult.Should()
                .NotBeNull()
                .And.
                BeEquivalentTo(user);
        }

        [Fact]
        public async Task GetUserByEmail_Should_Return_Result()
        {
            //Arrange
            var user = await ArrangeUser();

            //Act
            var actualResult = await UserProvider.GetUserByEmail(user.Email, CancellationToken);

            // Assert
            actualResult.Should()
                .NotBeNull()
                .And.
                BeEquivalentTo(user);
        }

        public async Task<User> ArrangeUser()
        {
            var roles = await RoleProvider.GetAll(CancellationToken);

            var createUserModel = Builder<CreateUserModel>
                .CreateNew()
                .With(x => x.RoleIds = roles.Select(x => x.Id))
                .Build();

            var user = await UserProvider.GetUserByEmail(createUserModel.Email, CancellationToken) ??
                await UserService.CreateUser(createUserModel, CancellationToken);

            return user;
        }
    }
}
