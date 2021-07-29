using System;

namespace Framework.Utils
{
    public abstract class DateTimeProvider
    {
        public static DateTimeProvider Current;

        public abstract DateTime Now { get; }
    }
}
