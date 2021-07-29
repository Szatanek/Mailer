using System.Threading;
using Framework.Services.Event;
using Mailer.Domain.Mail;

namespace Mailer.Services.Mail
{
    public sealed class MailStatusChangedEventHandler : EventHandlerBase<MailStatusChangedEvent>
    {
        private readonly SendMailService sendMailService;

        public MailStatusChangedEventHandler(SendMailService sendMailService)
        {
            this.sendMailService = sendMailService;
        }

        public override void Handle(MailStatusChangedEvent @event)
        {
            var cancellationToken = new CancellationToken();
            if ((MailStatus)@event.Status == MailStatus.Retrying)
            {
                sendMailService.SendMailAsync((MailGuid)@event.AggregateId, cancellationToken)
                    .Wait();
            }
        }
    }
}
