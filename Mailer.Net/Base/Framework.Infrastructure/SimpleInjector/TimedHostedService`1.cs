using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Framework.Infrastructure.SimpleInjector
{
    public sealed partial class TimedHostedService<TService> : IHostedService, IDisposable
        where TService : class
    {
        private readonly Container container;
        private readonly HostedServiceSettings<TService> settings;
        private readonly ILogger logger;
        private readonly Timer timer;

        public TimedHostedService(Container container, HostedServiceSettings<TService> settings, ILogger logger)
        {
            this.container = container;
            this.settings = settings;
            this.logger = logger;
            timer = new Timer(callback: _ => DoWork());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Verify that TService can be resolved
            container.GetRegistration(typeof(TService), true);
            // Start the timer
            timer.Change(dueTime: TimeSpan.Zero, period: settings.Interval);
            return Task.CompletedTask;
        }

        private void DoWork()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            try
            {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    var service = container.GetInstance<TService>();
                    settings.Action(service);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            finally
            {
                timer.Change(dueTime: settings.Interval, period: settings.Interval);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            return Task.CompletedTask;
        }

        public void Dispose() => timer.Dispose();
    }
}
