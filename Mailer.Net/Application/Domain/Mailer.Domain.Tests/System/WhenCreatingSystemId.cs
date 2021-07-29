using FluentAssertions;
using Mailer.Domain.System;
using Xunit;

namespace Mailer.Domain.Tests.Mail
{
    public sealed class WhenCreatingSystemId
    {
        [Theory]
        [InlineData(-100)]
        [InlineData(0)]
        [InlineData(20)]
        public void ShouldCreateNewMailGuidFromExistingValue(int value)
        {
            // Arrange

            // Act
            var systemId = (SystemId)value;

            // Assert
            systemId.Should().NotBeNull();
            ((int)systemId).Should().Be(value);
        }
    }
}
