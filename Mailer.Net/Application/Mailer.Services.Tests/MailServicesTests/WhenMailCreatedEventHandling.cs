using System;
using FluentAssertions;
using Mailer.Domain.Mail;
using Mailer.Services.Mail;
using Xunit;

namespace Mailer.Services.Tests.MailServicesTests
{
    public sealed class WhenMailCreatedEventHandling : BaseIntegrationTest
    {
        private readonly Guid mailId = Guid.NewGuid();
        private readonly MailCreatedEventHandler handler;

        public WhenMailCreatedEventHandling()
        {
            handler = GetService<MailCreatedEventHandler>();
        }

        [Fact]
        public void ShouldSendValidMessage()
        {
            // Given
            var @event = new MailCreatedEvent(mailId, "Topic", "Sender", "Recipient", 0);

            // When
            handler.Handle(@event);

            // Then
            var mail = Reader.GetMail(mailId);
            mail.Should().NotBeNull();
            mail.Status.Should().Be(MailStatus.Sent);
        }

        protected override void Seed()
        {
            const string to = "to@test.pl";
            const string from = "from@test.pl";
            const string topic = "Test WhenMailCreatedEventHandling";
            const string content = "<h1>Hello from <b>Test</b> case</h1><br><br><section>Test WhenMailCreatedEventHandling</section>";
            const bool isHtml = true;
            const int systemId = 5;
            var status = MailStatus.New;

            Builder
                .AddMail(mailId, to, from, topic, content, (byte)status, systemId, isHtml)
                .Build();
        }
    }
}
