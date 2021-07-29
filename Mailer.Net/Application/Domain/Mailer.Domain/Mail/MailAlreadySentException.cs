using System;
using System.Runtime.Serialization;
using Framework.Domain;

namespace Mailer.Domain.Mail
{
    [Serializable]
    public sealed class MailAlreadySentException : DomainException
    {
        public MailAlreadySentException()
            : base("Mail has been already sent.")
        {
        }

        private MailAlreadySentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}