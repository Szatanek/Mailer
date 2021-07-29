using FluentAssertions;
using Mailer.Domain.Mail;
using Mailer.Domain.System;
using Xunit;

namespace Mailer.Domain.Tests.Mail
{
    public sealed class WhenCreatingMail : BaseUnitTest
    {
        [Fact]
        public void ShouldCreateNewMail()
        {
            // Arrange
            var mailId = MailGuid.New();
            var topic = (MailTopic)"Test e-mail";
            var sender = (MailAddress)"test@test.pl";
            var recipient = (MailRecipient)"test@test.pl";
            var body = new MailBody("To jest jakiś e-mail.", false);
            var systemId = (SystemId)5;

            // Act
            var mail = new MailEntity(mailId, topic, sender, recipient, body, systemId);

            // Assert
            mail.Should().NotBeNull();
            mail.MailGuid.Should().Be(mailId);
            mail.Topic.Should().Be(topic);
            mail.Sender.Should().Be(sender);
            mail.Recipient.Should().Be(recipient);
            mail.SystemId.Should().Be(systemId);
            mail.Status.Should().Be(MailStatus.New);
            mail.Timestamp.Should().Be(Now);
            mail.Body.Should().Be(body);
        }

        [Fact]
        public void ShouldCreateEvent()
        {
            // Arrange
            var mailId = MailGuid.New();
            var topic = (MailTopic)"Test e-mail";
            var sender = (MailAddress)"test@test.pl";
            var recipient = (MailRecipient)"test@test.pl";
            var content = "To jest jakiś e-mail.";
            var isHtml = false;
            var body = new MailBody(content, isHtml);
            var systemId = (SystemId)5;

            // Act
            var mail = new MailEntity(mailId, topic, sender, recipient, body, systemId);

            // Assert
            mail.Should().NotBeNull();
            mail.Events.Should().NotBeNullOrEmpty();
            mail.Events.Should().ContainSingle(e => e is MailCreatedEvent);
        }
    }
}
