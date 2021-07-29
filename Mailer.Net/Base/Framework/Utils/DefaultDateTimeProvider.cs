using System;

namespace Framework.Utils
{
    public sealed class DefaultDateTimeProvider : DateTimeProvider
    {
        public override DateTime Now => DateTime.Now;
    }
}
