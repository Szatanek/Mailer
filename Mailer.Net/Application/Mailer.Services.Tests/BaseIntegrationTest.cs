using Framework.Infrastructure.Serialization;
using Framework.Infrastructure.SimpleInjector;
using Framework.Services.Event;
using Framework.Tests;
using Framework.Utils;
using Mailer.Infrastructure;
using Mailer.Services.Mail;
using Mailer.Services.Tests.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;

namespace Mailer.Services.Tests
{
    public abstract class BaseIntegrationTest
    {
        private readonly Container container = new Container();
        private readonly IConfiguration configuration;

        private IServiceCollection serviceCollection;
        private Scope scope;

        protected BaseIntegrationTest()
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile("testSettings.json")
                .Build();

            RegisterDependencies();
            Seed();
        }

        protected T GetService<T>() => scope.GetService<T>();

        internal MailerDataBuilder Builder => GetService<MailerDataBuilder>();
        
        internal MailerDataReader Reader => GetService<MailerDataReader>();

        protected DateTime Now => new DateTime(2020, 04, 15, 12, 31, 48);

        protected TestSettings Settings => configuration.GetSection("Settings").Get<TestSettings>();

        protected virtual void Seed()
        {
            Builder.Build();
        }

        protected void SetUpDateTime(DateTime staticDateTime)
        {
            DateTimeProvider.Current = new StaticDateTimeProvider(staticDateTime);
        }

        private void RegisterDependencies()
        {
            container.Options.AllowOverridingRegistrations = true;
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            SetUpDateTime(Now);
            SerializationProvider.Current = new JsonSerializationProvider();

            serviceCollection = new ServiceCollection()
                .AddLogging()
                .AddScoped<IEventQueue, InMemoryEventQueue>()
                .AddSingleton(Settings)
                .AddSingleton<IMailerDependencySettings>(Settings)
                .AddScoped<MailerDataBuilder>()
                .AddScoped<MailerDataReader>()
                .AddSimpleInjector(container);

            SimpleInjectorCommandHandlerRegistrar.RegisterCommandBus(container, typeof(SendMailCommandHandler).Assembly);
            SimpleInjectorQueryHandlerRegistrar.RegisterQueryDispatcher(container, typeof(GetMailsQueryHandler).Assembly);
            MailerDependencyRegistryProvider.RegisterFramework(serviceCollection, true, Settings);

            foreach(var (type1, type2) in MailerDependencyRegistryProvider.GetServices())
            {
                serviceCollection.AddScoped(type1, type2);
            }

            var provider = serviceCollection.BuildServiceProvider(true);
            provider.UseSimpleInjector(container);
            container.Verify();

            scope = AsyncScopedLifestyle.BeginScope(container);
        }
    }
}
