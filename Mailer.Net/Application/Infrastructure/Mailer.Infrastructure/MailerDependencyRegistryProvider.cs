using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Framework.Services.Event;
using Mailer.Domain.Mail;
using Mailer.Domain.User;
using Mailer.Infrastructure.Clients;
using Mailer.Infrastructure.Persistance;
using Mailer.Infrastructure.Repositories;
using Mailer.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mailer.Infrastructure
{
    public static class MailerDependencyRegistryProvider
    {
        public static void RegisterFramework(IServiceCollection services, bool isDevelopment, IMailerDependencySettings settings)
        {
            services.AddDbContext<MailerContext>((sp, options) =>
                {
                    options.UseSqlServer(settings.ConnectionString)
                        .UseLoggerFactory(sp.GetRequiredService<ILoggerFactory>())
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                        .EnableDetailedErrors(isDevelopment)
                        .EnableSensitiveDataLogging(isDevelopment);
                })
                .AddScoped<IDbConnection>(sp => new SqlConnection(settings.ConnectionString))
                .AddSingleton(settings);
        }

        public static IEnumerable<(Type, Type)> GetServices()
        {
            yield return (typeof(IEventRepository), typeof(MailerEventRepository));
            yield return (typeof(IUserRepository), typeof(UserRepository));
            yield return (typeof(IMailRepository), typeof(MailRepository));
            yield return (typeof(IMailClient), typeof(MailSmtpClient));
            yield return (typeof(IMailIntervalPolicy), typeof(MailIntervalPolicy));
        }
    }
}
