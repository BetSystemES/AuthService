using FizzWare.NBuilder;
using AuthService.BusinessLogic.Extensions;
using AuthService.BusinessLogic.Models.Enums;
using AuthService.UnitTests.Infrastructure;

namespace AuthService.UnitTests.Extensions
{
    [Trait(Constants.Category, Constants.UnitTest)]
    public class AuthRoleExtensionsTests
    {
        [Fact]
        public void GetDescriptionTest()
        {
            // Arrange
            var role = Builder<AuthRole>
                .CreateNew()
                .Build();

            var description = "admin";

            // Act
            var result = role.GetDescription();

            // Assert
            Assert.Equal(description, result);
        }

        [Fact]
        public void GetDescriptionArgumentNullExceptionTest()
        {
            // Arrange
            var role = (AuthRole)3;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => role.GetDescription());
        }
    }
}
