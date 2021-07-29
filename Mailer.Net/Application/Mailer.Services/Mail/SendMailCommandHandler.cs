using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Services.Command;
using Mailer.Domain.Mail;
using Mailer.Domain.System;
using Mailer.Services.Contracts.Write;

namespace Mailer.Services.Mail
{
    public sealed class SendMailCommandHandler : IAsyncCommandHandler<SendMailCommand>
    {
        private readonly IMailRepository mailRepository;

        public SendMailCommandHandler(IMailRepository mailRepository)
        {
            this.mailRepository = mailRepository;
        }

        public async Task HandleAsync(SendMailCommand command, CancellationToken cancellationToken)
        {
            var mailAddresses = command.Recipients.Select(r => (MailAddress)r);
            var body = new MailBody(command.Content, command.IsHtml);
            var mailEntity = new MailEntity(
                (MailGuid)command.Id,
                (MailTopic)command.Topic,
                (MailAddress)command.Sender,
                MailRecipient.From(mailAddresses),
                body,
                (SystemId)command.SystemId);

            await mailRepository.SaveAsync(mailEntity, cancellationToken);
        }
    }
}
