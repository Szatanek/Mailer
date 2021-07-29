using FluentAssertions;
using Mailer.Domain.Mail;
using Xunit;

namespace Mailer.Domain.Tests.Mail.Response
{
    public sealed class WhenCreatingMailErrorResponse : BaseUnitTest
    {
        [Fact]
        public void ShouldCreateNewMailErrorResponse()
        {
            // Arrange
            const string expectedErrorMessage = "Error";
            const string expectedErrorType = "System.Net.Smtp";

            // Act
            var mailResponse = MailResponseEntity.NewErrorResponse(expectedErrorMessage, expectedErrorType);

            // Assert
            mailResponse.Should().NotBeNull();
            mailResponse.ErrorMessage.Should().Be(expectedErrorMessage);
            mailResponse.ErrorType.Should().Be(expectedErrorType);
            mailResponse.Timestamp.Should().Be(Now);
        }
    }
}
