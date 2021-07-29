using FluentAssertions;
using Mailer.Domain.Mail;
using Mailer.Domain.System;
using Xunit;

namespace Mailer.Domain.Tests.Mail
{
    public sealed class WhenReconstitutingMail : BaseUnitTest
    {
        [Fact]
        public void ShouldCreateNewMail()
        {
            // Arrange
            const int id = 8;
            var mailId = MailGuid.New();
            var topic = (MailTopic)"Test e-mail";
            var sender = (MailAddress)"test@test.pl";
            var recipient = (MailRecipient)"test@test.pl";
            var body = new MailBody("To jest jakiś e-mail.", false);
            var status = MailStatus.Retrying;
            var systemId = (SystemId)5;
            var responses = new[] { MailResponseEntity.NewErrorResponse("Fatal error occured", "System.Net") };

            // Act
            var mail = MailEntity.Reconstitute(id, mailId, topic, sender, recipient, body, status, systemId, responses);

            // Assert
            mail.Should().NotBeNull();
            mail.MailGuid.Should().Be(mailId);
            mail.Topic.Should().Be(topic);
            mail.Sender.Should().Be(sender);
            mail.Recipient.Should().Be(recipient);
            mail.Status.Should().Be(status);
            mail.Body.Should().Be(body);
            mail.Status.Should().Be(status);
            mail.SystemId.Should().Be(systemId);
            mail.Responses.Should().BeEquivalentTo(responses);
        }

        [Fact]
        public void ShouldNotCreateEvent()
        {
            // Arrange
            const int id = 8;
            var mailId = MailGuid.New();
            var topic = (MailTopic)"Test e-mail";
            var sender = (MailAddress)"test@test.pl";
            var recipient = (MailRecipient)"test@test.pl";
            var content = "To jest jakiś e-mail.";
            var isHtml = false;
            var body = new MailBody(content, isHtml);
            var status = MailStatus.Retrying;
            var systemId = (SystemId)5;

            // Act
            var mail = MailEntity.Reconstitute(id, mailId, topic, sender, recipient, body, status, systemId);

            // Assert
            mail.Should().NotBeNull();
            mail.Events.Should().NotBeNull();
            mail.Events.Should().BeEmpty();
        }
    }
}
