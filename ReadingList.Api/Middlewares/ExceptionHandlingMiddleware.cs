using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ReadingList.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ExceptionToStatusCodeProvider exceptionToStatusCodeProvider)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e, exceptionToStatusCodeProvider);
            }
        }
        
        private static Task HandleExceptionAsync(HttpContext context, Exception exception,
            ExceptionToStatusCodeProvider exceptionToStatusCodeProvider)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) exceptionToStatusCodeProvider.GetStatusCode(exception.GetType());
            var result = JsonConvert.SerializeObject(new { errorMessage = exception.Message });
            return context.Response.WriteAsync(result);
        }
    }
}
