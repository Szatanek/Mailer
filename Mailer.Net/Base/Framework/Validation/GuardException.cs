using System;
using System.Runtime.Serialization;

namespace Framework.Validation
{
    [Serializable]
    public sealed class GuardException : Exception
    {
        public GuardException()
        {
        }

        private GuardException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}