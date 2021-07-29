using System;
using FluentAssertions;
using Framework.Validation;
using Mailer.Domain.Mail;
using Xunit;

namespace Mailer.Domain.Tests.Mail
{
    public sealed class WhenCreatingMailStatus
    {
        [Fact]
        public void ShouldCreateNewMailStatusFromFactoryMethod()
        {
            // Arrange
            const byte expectedValue = 1;

            // Act
            var mailStatus = MailStatus.Retrying;

            // Assert
            ((byte)mailStatus).Should().Be(expectedValue);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        public void ShouldCreateNewMailStatusFromValue(byte value)
        {
            // Arrange

            // Act
            var mailStatus = (MailStatus)value;

            // Assert
            ((byte)mailStatus).Should().Be(value);
        }

        [Theory]
        [InlineData(99)]
        [InlineData(4)]
        public void ShouldThrowForValueOutsideOfRange(byte value)
        {
            // Arrange

            // Act
            Func<MailStatus> createMailStatus = () => (MailStatus)value;

            // Assert
            createMailStatus.Should().Throw<GuardException>();
        }
    }
}
