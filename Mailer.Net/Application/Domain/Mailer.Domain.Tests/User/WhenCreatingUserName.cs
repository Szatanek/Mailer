using FluentAssertions;
using Mailer.Domain.User;
using Xunit;

namespace Mailer.Domain.Tests.User
{
    public sealed class WhenCreatingUserName
    {
        [Fact]
        public void ShouldReturnUserName()
        {
            // Arrange
            const string firstName = "Admin";
            const string lastName = "Istrator";
            const string value = "Admin Istrator";

            // Act
            var login = UserName.New(firstName, lastName);

            // Assert
            login.Should().NotBeNull();
            ((string)login).Should().Be(value);
        }
    }
}
