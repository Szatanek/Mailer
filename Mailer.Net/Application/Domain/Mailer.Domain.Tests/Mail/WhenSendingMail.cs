using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Mailer.Domain.Mail;
using Mailer.Domain.System;
using Moq;
using Xunit;

namespace Mailer.Domain.Tests.Mail
{
    public sealed class WhenSendingMail : BaseUnitTest
    {
        private readonly Mock<IMailRepository> mailRepositoryMock;
        private readonly Mock<IMailClient> mailClientMock;
        private readonly Mock<IMailIntervalPolicy> mailIntervalMock;

        private readonly SendMailService service;

        public WhenSendingMail()
            : base()
        {
            mailRepositoryMock = new Mock<IMailRepository>();
            mailClientMock = new Mock<IMailClient>();
            mailIntervalMock = new Mock<IMailIntervalPolicy>();

            service = new SendMailService(mailRepositoryMock.Object, mailIntervalMock.Object, mailClientMock.Object);
        }

        [Fact]
        public async Task ShouldChangeStatusOfMail()
        {
            // Arrange
            var mail = GetTestMail(MailStatus.New);
            mailRepositoryMock.Setup(r => r.GetAsync(It.Is<MailGuid>(m => m == mail.MailGuid), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mail));

            // Act
            await service.SendMailAsync(mail.MailGuid, CancellationToken.None);

            // Assert
            mail.Should().NotBeNull();
            mail.Status.Should().Be(MailStatus.Sent);
            mail.Events.Should().NotBeNullOrEmpty();
            mail.Events.Should().ContainSingle();
            mail.Events.Should().AllBeOfType<MailStatusChangedEvent>();
        }

        [Fact]
        public async Task ShouldSendMailViaMailClient()
        {
            // Arrange
            var mail = GetTestMail(MailStatus.New);
            mailRepositoryMock.Setup(r => r.GetAsync(It.Is<MailGuid>(m => m == mail.MailGuid), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mail));

            // Act
            await service.SendMailAsync(mail.MailGuid, CancellationToken.None);

            // Assert
            mailClientMock.Verify(
                c => c.SendAsync(It.Is<MailEntity>(m => m.Id == mail.Id), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task ShouldExecuteMailIntervalPolicy()
        {
            // Arrange
            var mail = GetTestMail(MailStatus.New);
            mailRepositoryMock.Setup(r => r.GetAsync(It.Is<MailGuid>(m => m == mail.MailGuid), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mail));

            // Act
            await service.SendMailAsync(mail.MailGuid, CancellationToken.None);

            // Assert
            mailIntervalMock.Verify(
                i => i.WaitUntilNextSendRequest(It.Is<MailEntity>(m => m.Id == mail.Id)),
                Times.Once);
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenMailIsAlreadySent()
        {
            // Arrange
            var mail = GetTestMail(MailStatus.Sent);
            mailRepositoryMock.Setup(r => r.GetAsync(It.Is<MailGuid>(m => m == mail.MailGuid), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mail));

            // Act
            Func<Task> SendMail = () => service.SendMailAsync(mail.MailGuid, CancellationToken.None);

            // Assert
            await SendMail.Should().ThrowAsync<MailAlreadySentException>();
        }

        [Fact]
        public async Task ShouldAddErrorResponseIfSendMailFailed()
        {
            // Arrange
            var expectedError = new InvalidOperationException("Test");
            var mail = GetTestMail(MailStatus.New);
            mailRepositoryMock.Setup(r => r.GetAsync(It.Is<MailGuid>(m => m == mail.MailGuid), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mail));

            mailClientMock.Setup(c => c.SendAsync(It.IsAny<MailEntity>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new SendMailException(expectedError));

            // Act
            await service.SendMailAsync(mail.MailGuid, CancellationToken.None);

            // Assert
            mail.Should().NotBeNull();
            mail.Responses.Should().NotBeNullOrEmpty();
            mail.Responses.Should().Contain(e => e.ErrorType == expectedError.GetType().FullName);
            mail.Events.Should().NotBeNullOrEmpty();
            mail.Events.Should().ContainSingle(e => e is MailResponseAddedEvent);
        }

        [Fact]
        public async Task ShouldChangeStatusWhenSendMailFailed()
        {
            // Arrange
            var expectedStatus = MailStatus.Retrying;
            var mail = GetTestMail(MailStatus.New);
            mailRepositoryMock.Setup(r => r.GetAsync(It.Is<MailGuid>(m => m == mail.MailGuid), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mail));

            mailClientMock.Setup(c => c.SendAsync(It.IsAny<MailEntity>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new SendMailException(new InvalidOperationException("Test")));

            // Act
            await service.SendMailAsync(mail.MailGuid, CancellationToken.None);

            // Assert
            mail.Should().NotBeNull();
            mail.Status.Should().Be(expectedStatus);
            mail.Events.Should().NotBeNullOrEmpty();
            mail.Events.Should().ContainSingle(e => e is MailStatusChangedEvent);
        }

        private static MailEntity GetTestMail(MailStatus status)
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

            return MailEntity.Reconstitute(id, mailId, topic, sender, recipient, body, status, systemId);
        }
    }
}
