using System;
using FluentAssertions;
using Mailer.Domain.Mail;
using Xunit;

namespace Mailer.Domain.Tests.Mail
{
    public sealed class WhenCreatingMailAddress
    {
        [Fact]
        public void ShouldCreateNewMailAddress()
        {
            // Arrange
            const string validEmailAddress = "test@test.pl";

            // Act
            var mailAddress = (MailAddress)validEmailAddress;

            // Assert
            mailAddress.Should().NotBeNull();
            ((string)mailAddress).Should().Be(validEmailAddress);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("")]
        [InlineData("test@euvic@test.pl")]
        [InlineData("test@pl")]
        public void ShouldThrowExceptionWhenMailAddressIsInvalid(string invalidEmailAddress)
        {
            // Arrange

            // Act
            Action createEmailAddress = () => 
            { 
                var emailAddress = (MailAddress)invalidEmailAddress; 
            };

            // Assert
            createEmailAddress.Should().Throw<InvalidMailAddressException>();
        }
    }
}
