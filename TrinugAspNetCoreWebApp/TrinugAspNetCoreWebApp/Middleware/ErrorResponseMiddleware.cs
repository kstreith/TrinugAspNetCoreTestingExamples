using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace TrinugAspNetCoreWebApp.Middleware
{
    public class ErrorResponseMiddleware
    {
        public ErrorResponseMiddleware(RequestDelegate ignore) { }

        public async Task Invoke(HttpContext context)
        {
            Exception exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (exception == null)
            {
                return;
            }
            var validationException = exception as ValidationException;
            if (validationException != null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var responseContent = JsonConvert.SerializeObject(validationException.Errors);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(responseContent);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
