using System.Collections.Generic;
using System.Linq;
using Framework.Domain;

namespace Mailer.Domain.Mail
{
    public sealed class MailRecipient : ValueObject<string, MailRecipient>
    {
        private const char Separator = ';';

        private MailRecipient(string value) 
            : base(value)
        {
        }

        public IEnumerable<MailAddress> MailAddresses => Value.Split(Separator).Select(address => (MailAddress)address);

        public static explicit operator MailRecipient(string value)
        {
            return new MailRecipient(value);
        }

        public static MailRecipient From(IEnumerable<MailAddress> mailAddresses)
        {
            return new MailRecipient(string.Join(Separator, mailAddresses.Select(m => (string)m)));
        }
    }
}