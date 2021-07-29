using System;
using System.Runtime.Serialization;
using Framework.Domain;

namespace Mailer.Domain.Mail
{
    [Serializable]
    public sealed class SendMailException : DomainException
    {
        public SendMailException(Exception ex)
            : base("Error when sending mail", ex)
        {
            SendErrorMessage = ex.Message;
            SendErrorType = ex.GetType().FullName;
        }

        private SendMailException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string SendErrorMessage { get; }

        public string SendErrorType { get; }
    }
}