using System.Runtime.Serialization;
using Framework.Domain;

namespace Mailer.Domain.Mail
{
    public sealed class InvalidMailTopicException : DomainException
    {
        public InvalidMailTopicException(string topic)
            : base("Invalid e-mail topic")
        {
            Topic = topic;
        }

        private InvalidMailTopicException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string Topic { get; }
    }
}