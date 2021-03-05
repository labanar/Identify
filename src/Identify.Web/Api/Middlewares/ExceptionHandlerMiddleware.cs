using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Identify.Web.Api.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Identify.Web.Api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        private static JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException validationException)
            {
                //validationException.AddHttpContextData(httpContext);
                _logger.LogWarning(validationException, validationException.Message);

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = "application/json";

                var errors = validationException.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                var errorResponse = new ErrorResponse(errors);
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse, _settings));
            }
            catch (Exception ex)
            {
                //ex.AddHttpContextData(httpContext);
                _logger.LogError(ex, ex.Message);

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json"; 

                var errorResponse = new ErrorResponse(new string[] { "Unexpected error" });
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse, _settings));
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
