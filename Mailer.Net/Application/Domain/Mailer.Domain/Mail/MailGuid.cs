using System;
using Framework.Domain;

namespace Mailer.Domain.Mail
{
    public sealed class MailGuid : ValueObject<Guid, MailGuid>
    {
        private MailGuid(Guid value) : base(value)
        {
        }

        public static explicit operator MailGuid(Guid value)
        {
            return new MailGuid(value);
        }

        public static MailGuid New()
        {
            return new MailGuid(Guid.NewGuid());
        }
    }
}