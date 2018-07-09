using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ReadingList.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ExceptionToStatusCodeProvider _exceptionToStatusCodeProvider;

        public ExceptionHandlingMiddleware(RequestDelegate next, ExceptionToStatusCodeProvider exceptionToStatusCodeProvider)
        {
            _next = next;
            _exceptionToStatusCodeProvider = exceptionToStatusCodeProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e) when (!(e is OperationCanceledException))
            {
                await HandleExceptionAsync(context, e);
            }
        }
        
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) _exceptionToStatusCodeProvider.GetStatusCode(exception.GetType());
            var result = JsonConvert.SerializeObject(new { errorMessage = exception.Message });
            return context.Response.WriteAsync(result);
        }
    }
}
