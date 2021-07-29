using System;
using Framework.Domain;

namespace Mailer.Domain.Mail
{
    public sealed class MailCreatedEvent : DomainEvent<Guid>
    {
        public MailCreatedEvent(Guid aggregateId, string topic, string sender, string recipient, byte status)
            : base(aggregateId)
        {
            Topic = topic;
            Sender = sender;
            Recipient = recipient;
            Status = status;
        }

        public string Topic { get; }

        public string Sender { get; }

        public string Recipient { get; }

        public byte Status { get; }
    }
}