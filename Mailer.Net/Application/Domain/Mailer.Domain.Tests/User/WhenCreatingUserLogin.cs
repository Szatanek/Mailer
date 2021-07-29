using FluentAssertions;
using Mailer.Domain.User;
using Xunit;

namespace Mailer.Domain.Tests.User
{
    public sealed class WhenCreatingUserLogin
    {
        [Fact]
        public void ShuldReturnUserLogin()
        {
            // Arrange
            const string value = "admin";

            // Act
            var login = (UserLogin)value;

            // Assert
            login.Should().NotBeNull();
            ((string)login).Should().Be(value);
        }
    }
}
