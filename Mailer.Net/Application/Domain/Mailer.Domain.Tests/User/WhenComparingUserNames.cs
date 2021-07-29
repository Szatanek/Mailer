using FluentAssertions;
using Mailer.Domain.User;
using Xunit;

namespace Mailer.Domain.Tests.User
{
    public sealed class WhenComparingUserNames
    {
        [Theory]
        [InlineData("Admin", "Istrator")]
        [InlineData("Admin", null)]
        [InlineData(null, "Istrator")]
        public void ShouldReturnTrueWhenComparingExactValues(string firstName, string lastName)
        {
            // Arrange
            var first = UserName.New(firstName, lastName);
            var second = UserName.New(firstName, lastName);

            // Act
            var result = first == second;
            var result2 = first.Equals(second);

            // Assert
            result.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnFalseWhenComparingDifferentValues()
        {
            // Arrange
            const string firstName1 = "Admin";
            const string firstName2 = "Admin1";
            const string lastName = "Istrator";
            var first = UserName.New(firstName1, lastName);
            var second = UserName.New(firstName2, lastName);

            // Act
            var result = first == second;
            var result2 = first.Equals(second);

            // Assert
            result.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnFalseWhenComparingNull()
        {
            // Arrange
            const string firstName = "Admin";
            const string lastName = "Istrator";
            var first = UserName.New(firstName, lastName);
            UserName second = null;

            // Act
            var result = first == second;
            var result2 = first.Equals(second);

            // Assert
            result.Should().BeFalse();
            result2.Should().BeFalse();
        }
    }
}
