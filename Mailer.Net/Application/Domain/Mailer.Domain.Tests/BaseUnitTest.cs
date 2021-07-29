using System;
using Framework.Tests;
using Framework.Utils;

namespace Mailer.Domain.Tests
{
    public abstract class BaseUnitTest
    {
        protected BaseUnitTest()
        {
            DateTimeProvider.Current = new StaticDateTimeProvider(Now);
        }

        protected DateTime Now => new DateTime(2019, 05, 29, 18, 32, 58);
    }
}