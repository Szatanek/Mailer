using System.Collections.Generic;
using FluentAssertions;
using Mailer.Domain.Mail;
using Mailer.Domain.System;
using Xunit;

namespace Mailer.Domain.Tests.Mail.Response
{
    public sealed class WhenAddingMailErrorResponse : BaseUnitTest
    {
        [Fact]
        public void ShouldAddMailResponse()
        {
            // Arrange
            const string errorMessage = "Sending mail Failed";
            const string errorType = "System.Net.Smtp";
            var mail = GetTestMail(MailStatus.New);

            // Act
            mail.AddErrorResponse(errorMessage, errorType);

            // Assert
            mail.Responses.Should().NotBeNullOrEmpty();
            mail.Events.Should().ContainSingle(e => e is MailResponseAddedEvent);
        }

        [Fact]
        public void ShouldChangeStatusToRetrying()
        {
            // Arrange
            const string errorMessage = "Sending mail Failed";
            const string errorType = "System.Net.Smtp";
            var mail = GetTestMail(MailStatus.New);

            // Act
            mail.AddErrorResponse(errorMessage, errorType);

            // Assert
            mail.Status.Should().Be(MailStatus.Retrying);
            mail.Events.Should().ContainSingle(e => e is MailStatusChangedEvent);
        }

        [Fact]
        public void ShouldChangeStatusToFaulted()
        {
            // Arrange
            const string errorMessage = "Sending mail Failed";
            const string errorType = "System.Net.Smtp";
            var responses = GetTestResponses();
            var mail = GetTestMail(MailStatus.Retrying, responses);

            // Act
            mail.AddErrorResponse(errorMessage, errorType);

            // Assert
            mail.Status.Should().Be(MailStatus.Faulted);
            mail.Events.Should().ContainSingle(e => e is MailStatusChangedEvent);
        }

        private IEnumerable<MailResponseEntity> GetTestResponses()
        {
            yield return MailResponseEntity.Reconstitute(1, "Test", "System", Now);
            yield return MailResponseEntity.Reconstitute(2, "Test", "System", Now);
            yield return MailResponseEntity.Reconstitute(3, "Test", "System", Now);
            yield return MailResponseEntity.Reconstitute(4, "Test", "System", Now);
        }

        private static MailEntity GetTestMail(MailStatus status, IEnumerable<MailResponseEntity> responses = null)
        {
            const int id = 5;
            var mailId = MailGuid.New();
            var topic = (MailTopic)"Test e-mail";
            var sender = (MailAddress)"test@test.pl";
            var recipient = (MailRecipient)"test@test.pl";
            var content = "To jest jakiś e-mail.";
            var isHtml = false;
            var body = new MailBody(content, isHtml);
            var systemId = (SystemId)5;

            return MailEntity.Reconstitute(id, mailId, topic, sender, recipient, body, status, systemId, responses);
        }
    }
}
