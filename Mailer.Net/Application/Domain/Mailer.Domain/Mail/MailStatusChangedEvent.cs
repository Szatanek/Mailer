using System;
using Framework.Domain;

namespace Mailer.Domain.Mail
{
    public sealed class MailStatusChangedEvent : DomainEvent<Guid>
    {
        public MailStatusChangedEvent(Guid aggregateId, byte status)
            : base(aggregateId)
        {
            Status = status;
        }

        public byte Status { get; }
    }
}