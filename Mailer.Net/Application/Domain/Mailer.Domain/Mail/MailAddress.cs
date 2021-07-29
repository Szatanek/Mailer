using System.Text.RegularExpressions;
using Framework.Domain;

namespace Mailer.Domain.Mail
{
    public sealed class MailAddress : ValueObject<string, MailAddress>
    {
        private const int MaxAddressLength = 50;
        private const string ValidationPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        private static readonly Regex EmailRegex = new Regex(ValidationPattern, RegexOptions.IgnoreCase);

        private MailAddress(string value) 
            : base(value)
        {
            Validate(Value);
        }

        public static explicit operator MailAddress(string value)
        {
            return new MailAddress(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length > MaxAddressLength)
            {
                throw new InvalidMailAddressException(value);
            }

            if (!EmailRegex.IsMatch(value))
            {
                throw new InvalidMailAddressException(value);
            }
        }
    }
}