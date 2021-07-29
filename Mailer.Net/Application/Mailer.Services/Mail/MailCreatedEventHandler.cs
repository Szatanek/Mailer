using System.Threading;
using Framework.Services.Event;
using Mailer.Domain.Mail;

namespace Mailer.Services.Mail
{
    public sealed class MailCreatedEventHandler : EventHandlerBase<MailCreatedEvent>
    {
        private readonly SendMailService sendMailService;

        public MailCreatedEventHandler(SendMailService sendMailService)
        {
            this.sendMailService = sendMailService;
        }

        public override void Handle(MailCreatedEvent @event)
        {
            var cancellationToken = new CancellationToken();
            sendMailService.SendMailAsync((MailGuid)@event.AggregateId, cancellationToken)
                .Wait();
        }
    }
}
