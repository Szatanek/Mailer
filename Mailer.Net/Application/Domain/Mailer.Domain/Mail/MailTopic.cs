using Framework.Domain;

namespace Mailer.Domain.Mail
{
    public sealed class MailTopic : ValueObject<string, MailTopic>
    {
        private const int TopicMaxLength = 250;

        private MailTopic(string value)
            : base(value)
        {
            Validate(Value);
        }

        public static explicit operator MailTopic(string value)
        {
            return new MailTopic(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidMailTopicException(value);
            }

            if (value.Length > TopicMaxLength)
            {
                throw new InvalidMailTopicException(value);
            }
        }
    }
}