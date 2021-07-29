using System;
using Framework.Domain;
using Framework.Utils;

namespace Mailer.Domain.Mail
{
    public sealed class MailResponseEntity : DomainEntity
    {
        private MailResponseEntity()
        {
        }

        public int Id { get; private set; }

        public int MailId { get; private set; }

        public string ErrorMessage { get; private set; }

        public string ErrorType { get; private set; }

        public DateTime Timestamp { get; private set; }

        public MailEntity Mail { get; private set; }

        public static MailResponseEntity NewErrorResponse(string errorMessage, string errorType)
        {
            return new MailResponseEntity
            {
                ErrorMessage = errorMessage,
                ErrorType = errorType,
                Timestamp = DateTimeProvider.Current.Now,
            };
        }

        public static MailResponseEntity Reconstitute(int id, string errorMessage, string errorType, DateTime timestamp)
        {
            return new MailResponseEntity
            {
                Id = id,
                ErrorMessage = errorMessage,
                ErrorType = errorType,
                Timestamp = timestamp,
            };
        }
    }
}