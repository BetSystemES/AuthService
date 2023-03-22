using FizzWare.NBuilder;
using Moq;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Services;
using AuthService.UnitTests.Infrastructure;

namespace AuthService.UnitTests.Services
{
    [Trait(Constants.Category, Constants.UnitTest)]
    public class RoleServiceTests
    {
        private readonly Mock<IRoleProvider> _roleProvider;

        private readonly RoleService _target;

        public RoleServiceTests()
        {
            _roleProvider = new();

            _target = new(_roleProvider.Object);
        }

        [Fact]
        public async Task GetAllTest()
        {
            // Arrange
            var roles = Builder<Role>
                .CreateListOfSize(3)
                .Build();

            _roleProvider.Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(roles.ToList())
                .Verifiable();

            // Act
            var result = await _target.GetAll(It.IsAny<CancellationToken>());

            // Assert
            // TODO: please use library FluentAssertion
            Assert.Equal(roles, result);

            _roleProvider.Verify(x => x.GetAll(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
