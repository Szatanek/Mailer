using Framework.Domain;

namespace Mailer.Domain.Mail
{
    public sealed class MailBody : ValueObject<MailBody>
    {
        public MailBody(string content, bool isHtml)
        {
            Content = content;
            IsHtml = isHtml;
        }

        public string Content { get; private set; }

        public bool IsHtml { get; private set; }
    }
}