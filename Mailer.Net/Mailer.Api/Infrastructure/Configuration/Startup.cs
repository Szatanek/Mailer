using System;
using System.Text.Json.Serialization;
using Framework.Infrastructure.EventStore;
using Framework.Infrastructure.SimpleInjector;
using Framework.Services.Event;
using Framework.Utils;
using Mailer.Infrastructure;
using Mailer.Api.Infrastructure.Filters;
using Mailer.Services.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;

namespace Mailer.Api.Infrastructure.Configuration
{
    public sealed class Startup
    {
        private const string AllowLocalhost = "_allowOrigins";

        private readonly Container container = new Container();

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore(ConfigureMvc)
                .AddApiExplorer()
                .AddJsonOptions(config =>
                {
                    config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    config.JsonSerializerOptions.AllowTrailingCommas = true;
                });

            services.AddCors(options =>
            {
                var origin = Configuration.GetValue<string>("AllowOrigins");
                options.AddPolicy(AllowLocalhost,
                builder =>
                {
                    builder.WithOrigins(origin.Split(';'))
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST", "OPTIONS")
                        .AllowCredentials();
                });
            });

            var applicationConfiguration = ApplicationConfigurationProvider.GetConfiguration(Configuration);
            MailerDependencyRegistryProvider.RegisterFramework(
                services,
                Environment.IsDevelopment(),
                applicationConfiguration);

            services.AddSimpleInjector(container, options =>
            {
                options.AddHostedService<TimedHostedService<EventProcessor>>();
                options.AddAspNetCore()
                    .AddControllerActivation();
                options.AddLogging();
            });

            InitializeContainer();
            SwaggerRegistry.Register(services, Environment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSimpleInjector(container);
            app.UseCors(AllowLocalhost);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(config => config.MapControllers());

            container.Verify();
            SwaggerRegistry.Configure(app, env);
            DateTimeProvider.Current = new DefaultDateTimeProvider();
        }

        private static void ConfigureMvc(MvcOptions config)
        {
            config.Filters.Add(typeof(ApiExceptionFilter));
        }

        private void InitializeContainer()
        {
            container.RegisterInstance(new HostedServiceSettings<EventProcessor>(
                interval: TimeSpan.FromMilliseconds(100),
                action: service => service.Process()));

            container.Register<IEventSerializer, JsonEventSerializer>(Lifestyle.Singleton);
            container.Register<IEventQueue, InMemoryEventQueue>(Lifestyle.Singleton);
            container.Register<EventStore>(Lifestyle.Scoped);

            SimpleInjectorEventHandlerRegistrar.RegisterEventHandling(container, typeof(MailCreatedEventHandler).Assembly);
            SimpleInjectorCommandHandlerRegistrar.RegisterCommandBus(container, typeof(SendMailCommandHandler).Assembly);
            SimpleInjectorQueryHandlerRegistrar.RegisterQueryDispatcher(container, typeof(GetMailsQueryHandler).Assembly);

            foreach (var service in MailerDependencyRegistryProvider.GetServices())
            {
                container.Register(service.Item1, service.Item2, Lifestyle.Scoped);
            }
        }
    }
}
