using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Helpers
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ErrorLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exp) {
                _logger.LogError(
                      "Request {method} {url} => error: {exp}",
                      context.Request?.Method,
                      context.Request?.Path.Value,
                      exp.Message);
            }
            finally
            {
                //_logger.LogInformation(
                //    "Request {method} {url} => status: {statusCode}",
                //    context.Request?.Method,
                //    context.Request?.Path.Value,
                //    context.Response?.StatusCode);
            }
        }
    }
}
