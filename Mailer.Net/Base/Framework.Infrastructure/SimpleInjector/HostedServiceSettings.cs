using System;

namespace Framework.Infrastructure.SimpleInjector
{
    public sealed class HostedServiceSettings<TService>
        where TService : class
    {
        public HostedServiceSettings(TimeSpan interval, Action<TService> action)
        {
            Interval = interval;
            Action = action;
        }

        public TimeSpan Interval { get; }

        public Action<TService> Action { get; }
    }
}
