using Framework.Utils;
using System;

namespace Framework.Tests
{
    public sealed class StaticDateTimeProvider : DateTimeProvider
    {
        private DateTime date;

        public StaticDateTimeProvider(DateTime date)
        {
            this.date = date;
        }

        public override DateTime Now => date;

        public void Change(DateTime date)
        {
            this.date = date;
        }
    }
}
