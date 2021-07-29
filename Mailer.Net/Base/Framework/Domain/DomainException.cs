using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Framework.Domain
{
    [Serializable]
    [DebuggerStepThrough]
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }

        protected DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DomainException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        public bool ShowOnUi => true;
    }
}