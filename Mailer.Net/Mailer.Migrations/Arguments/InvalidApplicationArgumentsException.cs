using System;
using System.Runtime.Serialization;

namespace Mailer.Migrations.Arguments
{
    [Serializable]
    internal sealed class InvalidApplicationArgumentsException : Exception
    {
        public InvalidApplicationArgumentsException()
            : base("Missing application arguments.")
        {
        }

        private InvalidApplicationArgumentsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}