using System;
using Framework.Domain;

namespace Mailer.Domain.Mail
{
    public sealed class MailResponseAddedEvent : DomainEvent<Guid>
    {
        public MailResponseAddedEvent(Guid aggregateId, string sendErrorMessage, string sendErrorType) 
            : base(aggregateId)
        {
            SendErrorMessage = sendErrorMessage;
            SendErrorType = sendErrorType;
        }

        public string SendErrorMessage { get; }
        public string SendErrorType { get; }
    }
}