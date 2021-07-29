using Framework.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleInjector;
using System;
using System.Net;

namespace Mailer.Api.Infrastructure.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private const string DefaultExceptionMessage = "Wystąpił nieprzewidziany błąd serwera.";
        private const string ResponseContentType = "application/json";

        private readonly ILogger logger;

        public ApiExceptionFilter(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<Exception>();
        }

        public override void OnException(ExceptionContext context)
        {
            HttpResponse response = context.HttpContext.Response;
            response.ContentType = ResponseContentType;
            string errorMessage;
            string result;
            var model = string.Empty;
            var isError = true;

            if (context.Exception is DomainException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorMessage =
                result = JsonConvert.SerializeObject(
                    new
                    {
                        message = context.Exception.Message,
                        errorCode = response.StatusCode,
                        isError,
                        model
                    });
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorMessage = DefaultExceptionMessage;
                result = JsonConvert.SerializeObject(
                    new
                    {
                        message = errorMessage,
                        errorCode = response.StatusCode,
                        isError,
                        model
                    });
            }

            logger.Log(LogLevel.Error, errorMessage);
            response.WriteAsync(result);
        }
    }
}