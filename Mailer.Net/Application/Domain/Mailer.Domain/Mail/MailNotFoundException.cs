using System;
using System.Runtime.Serialization;
using Framework.Domain;

namespace Mailer.Infrastructure.Repositories
{
    [Serializable]
    public sealed class MailNotFoundException : DomainException
    {
        public MailNotFoundException()
            : base("Mail not found")
        {
        }

        private MailNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}