using System;
using FluentAssertions;
using Mailer.Domain.Mail;
using Xunit;

namespace Mailer.Domain.Tests.Mail
{
    public sealed class WhenCreatingMailGuid
    {
        [Fact]
        public void ShouldCreateNewMailGuidFromScratch()
        {
            // Arrange

            // Act
            var mailGuid = MailGuid.New();

            // Assert
            mailGuid.Should().NotBeNull();
            ((Guid)mailGuid).Should().NotBeEmpty();
        }

        [Theory]
        [InlineData("181d44ea-b5a0-4e66-8af6-ebca14c6fdce")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void ShouldCreateNewMailGuidFromExistingValue(string stringValue)
        {
            // Arrange
            var value = Guid.Parse(stringValue);

            // Act
            var mailGuid = (MailGuid)value;

            // Assert
            mailGuid.Should().NotBeNull();
            ((Guid)mailGuid).Should().Be(value);
        }
    }
}
