using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Mailer.Api.Infrastructure.Configuration
{
    internal static class SwaggerRegistry
    {
        internal static void Register(IServiceCollection services, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                return;
            }

            services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", CreateInfoForApiVersion());
             });
        }

        private static OpenApiInfo CreateInfoForApiVersion()
        {
            return new OpenApiInfo
            {
                Title = "Mailer API v1",
                Description = "API for Mailer application",
                Version = "1"
            };
        }

        internal static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                return;
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mailer API");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
