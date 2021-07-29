using System;
using System.Runtime.Serialization;
using Framework.Domain;

namespace Mailer.Domain.User
{
    [Serializable]
    public sealed class UserNotFoundException : DomainException
    {
        public UserNotFoundException()
            : base("User not found")
        {
        }

        private UserNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}