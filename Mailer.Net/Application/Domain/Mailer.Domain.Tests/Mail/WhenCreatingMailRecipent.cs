using System;
using FluentAssertions;
using Mailer.Domain.Mail;
using Xunit;

namespace Mailer.Domain.Tests.Mail
{
    public sealed class WhenCreatingMailRecipient
    {
        [Fact]
        public void ShouldCreateNewMailRecipientFromValue()
        {
            // Arrange
            const string validEmailAddress = "test@test.pl";

            // Act
            var mailRecipient = (MailRecipient)validEmailAddress;

            // Assert
            mailRecipient.Should().NotBeNull();
            ((string)mailRecipient).Should().Be(validEmailAddress);
        }

        [Fact]
        public void ShouldCreateNewMailRecipientFromAddressesCollection()
        {
            // Arrange
            const string separator = ";";
            var mail1 = (MailAddress)"test1@test.pl";
            var mail2 = (MailAddress)"test2@test.pl";
            var addresses = new[] { mail1, mail2 };

            // Act
            var mailRecipient = MailRecipient.From(addresses);

            // Assert
            mailRecipient.Should().NotBeNull();
            ((string)mailRecipient).Should().StartWith((string)mail1);
            ((string)mailRecipient).Should().EndWith((string)mail2);
            ((string)mailRecipient).Should().Contain(separator);
        }
    }
}
