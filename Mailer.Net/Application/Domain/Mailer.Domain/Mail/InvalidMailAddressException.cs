using System.Runtime.Serialization;
using Framework.Domain;

namespace Mailer.Domain.Mail
{
    public sealed class InvalidMailAddressException : DomainException
    {
        public InvalidMailAddressException(string emailAddress)
            : base("Invalid e-mail address")
        {
            EmailAddress = emailAddress;
        }

        private InvalidMailAddressException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string EmailAddress { get; }
    }
}