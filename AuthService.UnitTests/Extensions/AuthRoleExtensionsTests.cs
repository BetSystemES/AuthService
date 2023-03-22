using FizzWare.NBuilder;
using AuthService.BusinessLogic.Extensions;
using AuthService.BusinessLogic.Models.Enums;
using AuthService.UnitTests.Infrastructure;

using FluentAssertions;

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
            result.Should().Be(description);
        }

        [Fact]
        public void GetDescriptionArgumentNullExceptionTest()
        {
            // Arrange
            var role = (AuthRole)3;

            // Act
            Action result = () => role.GetDescription();

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }
    }
}
