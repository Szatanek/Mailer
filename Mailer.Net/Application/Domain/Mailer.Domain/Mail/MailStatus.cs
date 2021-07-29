using Framework.Domain;
using Framework.Validation;

namespace Mailer.Domain.Mail
{
    public sealed class MailStatus : Enumeration<byte, MailStatus>
    {
        private MailStatus(byte value, string name)
            : base(value, name)
        {
        }

        public static MailStatus New => new MailStatus(0, nameof(New));

        public static MailStatus Retrying => new MailStatus(1, nameof(Retrying));

        public static MailStatus Sent => new MailStatus(2, nameof(Sent));

        public static MailStatus Faulted => new MailStatus(3, nameof(Faulted));

        public static explicit operator MailStatus(byte value)
        {
            Guard.InRange(value, 0, 3);
            return Get(value);
        }
    }
}
