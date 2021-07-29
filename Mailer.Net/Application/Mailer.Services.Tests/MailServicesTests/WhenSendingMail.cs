using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Framework.Services.Command;
using Mailer.Domain.Mail;
using Mailer.Services.Contracts.Write;
using Xunit;

namespace Mailer.Services.Tests.MailServicesTests
{
    public sealed class WhenSendingMail : BaseIntegrationTest
    {
        private readonly IApplicationCommandBus commandBus;

        public WhenSendingMail()
        {
            commandBus = GetService<IApplicationCommandBus>();
        }

        [Fact]
        public async Task ShouldAddMailToDatabase()
        {
            // Given
            var command = new SendMailCommand
            {
                Topic = "Test Mail",
                Sender = "test@test.pl",
                Recipients = new[] { "recipient1@test.pl", "recipient2@test.pl" },
                Content = "<h1>Hello from <b>Test</b> case</h1><br><br><section>Test Content</section>",
                IsHtml = true,
                SystemId = 5,
            };

            // When
            await commandBus.HandleAsync(command, CancellationToken.None);

            // Then
            var mail = Reader.GetMail(command.Id);
            mail.Should().NotBeNull();
            mail.Status.Should().Be(MailStatus.New);
            mail.Timestamp.Should().Be(Now);
            ((string)mail.Topic).Should().Be(command.Topic);
            ((string)mail.Sender).Should().Be(command.Sender);
            ((int)mail.SystemId).Should().Be(command.SystemId);

            var mailBody = Reader.GetMailBody(command.Id);
            mailBody.Should().NotBeNull();
            mailBody.Content.Should().Be(command.Content);
            mailBody.IsHtml.Should().Be(command.IsHtml);
        }
    }
}
