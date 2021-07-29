using System;
using System.Diagnostics;
using FluentAssertions;
using Mailer.Domain.Mail;
using Mailer.Services.Mail;
using Xunit;

namespace Mailer.Services.Tests.MailServicesTests
{
    public sealed class WhenMailStatusChangedEventHandling : BaseIntegrationTest
    {
        private readonly Guid mailId = Guid.NewGuid();
        private readonly Guid invalidMailId = Guid.NewGuid();
        private readonly MailStatusChangedEventHandler handler;

        public WhenMailStatusChangedEventHandling()
        {
            handler = GetService<MailStatusChangedEventHandler>();
        }

        [Fact]
        public void ShouldSendValidMessage()
        {
            // Given
            const int expectedResponsesCount = 1;
            var @event = new MailStatusChangedEvent(mailId, (byte)MailStatus.Retrying);

            // When
            handler.Handle(@event);

            // Then
            var mail = Reader.GetMail(mailId);
            mail.Should().NotBeNull();
            mail.Status.Should().Be(MailStatus.Sent);

            var responses = Reader.GetMailResponses(mailId);
            responses.Should().NotBeNullOrEmpty();
            responses.Should().HaveCount(expectedResponsesCount);
        }

        [Fact]
        public void ShouldWaitExpectedTimeUntilSendMail()
        {
            // Given
            var expectedTimeToWait = Settings.MailRetryIntervalMilliseconds;
            var @event = new MailStatusChangedEvent(mailId, (byte)MailStatus.Retrying);
            var timer = Stopwatch.StartNew();

            // When
            handler.Handle(@event);
            timer.Stop();

            // Then
            var mail = Reader.GetMail(mailId);
            mail.Should().NotBeNull();
            mail.Status.Should().Be(MailStatus.Sent);

            timer.ElapsedMilliseconds.Should().BeGreaterThan(expectedTimeToWait);
        }

        [Fact]
        public void ShouldNotRetryWhenMailIsInFaultedState()
        {
            // Given
            const int expectedResponsesCount = 2;
            var @event = new MailStatusChangedEvent(invalidMailId, (byte)MailStatus.Faulted);

            // When
            handler.Handle(@event);

            // Then
            var mail = Reader.GetMail(invalidMailId);
            mail.Should().NotBeNull();
            mail.Status.Should().Be(MailStatus.Faulted);

            var responses = Reader.GetMailResponses(invalidMailId);
            responses.Should().NotBeNullOrEmpty();
            responses.Should().HaveCount(expectedResponsesCount);
        }

        protected override void Seed()
        {
            const string to = "to@test.pl";
            const string from = "from@test.pl";
            const string topic = "Test WhenMailStatusChangedEventHandling";
            const string content = "<h1>Hello from <b>Test</b> case</h1><br><br><section>This mail should be sent WhenMailStatusChangedEventHandling</section>";
            const bool isHtml = true;
            const int systemId = 5;
            var status = MailStatus.Retrying;
            var invalidMailStatus = MailStatus.Faulted;

            Builder
                .AddMail(mailId, to, from, topic, content, (byte)status, systemId, isHtml: isHtml)
                .AddMailResponse(mailId, "Error", "System.Exception")
                .AddMail(invalidMailId, to, from, topic, content, (byte)invalidMailStatus, systemId, isHtml: isHtml)
                .AddMailResponse(invalidMailId, "Error", "System.Exception")
                .AddMailResponse(invalidMailId, "Error", "System.Exception")
                .Build();
        }
    }
}
