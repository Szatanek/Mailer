using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Domain;
using Framework.Utils;
using Mailer.Domain.System;

namespace Mailer.Domain.Mail
{
    public sealed class MailEntity : AggregateRoot
    {
        private const int RetriesCount = 5;

        private MailEntity()
        {
        }

        public MailEntity(MailGuid mailId, MailTopic topic, MailAddress sender, MailRecipient recipient, MailBody body, SystemId systemId)
        {
            MailGuid = mailId;
            Topic = topic;
            Sender = sender;
            Recipient = recipient;
            Body = body;
            SystemId = systemId;
            Status = MailStatus.New;
            Timestamp = DateTimeProvider.Current.Now;
            Responses = new List<MailResponseEntity>();

            Enqueue(new MailCreatedEvent((Guid)mailId, (string)topic, (string)sender, (string)recipient, (byte)Status));
        }

        public int Id { get; private set; }

        public MailGuid MailGuid { get; private set; }

        public MailTopic Topic { get; private set; }

        public MailAddress Sender { get; private set; }

        public MailRecipient Recipient { get; private set; }

        public MailStatus Status { get; private set; }

        public MailBody Body { get; private set; }

        public SystemId SystemId { get; private set; }
        
        public DateTime Timestamp { get; private set; }

        public ICollection<MailResponseEntity> Responses { get; private set; }

        public void MailSent()
        {
            Status = MailStatus.Sent;
            Timestamp = DateTimeProvider.Current.Now;
            Enqueue(new MailStatusChangedEvent((Guid)MailGuid, (byte)Status));
        }

        public void AddErrorResponse(string sendErrorMessage, string sendErrorType)
        {
            var response = MailResponseEntity.NewErrorResponse(sendErrorMessage, sendErrorType);
            Responses.Add(response);
            if (Responses.Count < RetriesCount)
            {
                Status = MailStatus.Retrying;
            }
            else
            {
                Status = MailStatus.Faulted;
            }

            Timestamp = DateTimeProvider.Current.Now;
            Enqueue(new MailStatusChangedEvent((Guid)MailGuid, (byte)Status));
            Enqueue(new MailResponseAddedEvent((Guid)MailGuid, sendErrorMessage, sendErrorType));
        }

        public static MailEntity Reconstitute(
            int id,
            MailGuid mailId,
            MailTopic topic,
            MailAddress sender,
            MailRecipient recipient,
            MailBody body,
            MailStatus status,
            SystemId systemId,
            IEnumerable<MailResponseEntity> responses = null)
        {
            return new MailEntity
            {
                Id = id,
                MailGuid = mailId,
                Topic = topic,
                Sender = sender,
                Recipient = recipient,
                Body = body,
                Status = status,
                SystemId = systemId,
                Responses = responses == null ? new List<MailResponseEntity>() : responses.ToList(),
            };
        }
    }
}