using KNews.Identity.Services.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace KNews.Identity.WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex is BaseResponseException)
                    await HandleExceptionAsync(httpContext, (BaseResponseException)ex);
                else
                    await HandleExceptionAsync(httpContext, ex);

            }
        }

        private Task HandleExceptionAsync(HttpContext context, BaseResponseException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var errorJsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(new TextEncoderSettings())
            };
            
            var body = JsonSerializer.Serialize(
                new
                {
                    Error = new
                    {
                        Code = exception.Code,
                        Message = exception.Message,
                        Data = exception.ErrorData
                    }
                }, errorJsonOptions);
            _logger.LogError(exception, body);
            return context.Response.WriteAsync(body, Encoding.UTF8);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var body = JsonSerializer.Serialize(
                new
                {
                    Error = new { Message = exception.Message }
                });
            _logger.LogError(exception, exception.Message);
            return context.Response.WriteAsync(body);
        }
    }
}
