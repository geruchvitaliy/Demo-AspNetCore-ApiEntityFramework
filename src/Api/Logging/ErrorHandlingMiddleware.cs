using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Logging
{
    class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //TODO: Log error

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetResponseStatusCode(exception);

            return context.Response.WriteAsync(result);
        }

        private static HttpStatusCode GetResponseStatusCode(Exception exception)
        {
            if (exception.GetType() == typeof(Common.Exceptions.EntityNotFoundException<>))
                return HttpStatusCode.NotFound;
            else
                return HttpStatusCode.InternalServerError;
        }
    }
}