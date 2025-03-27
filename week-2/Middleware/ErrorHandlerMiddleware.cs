using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace week_2_assignment.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {  // Continue processing the request
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // Catch the exception
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json"; // Set the response content type
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Set the status code

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Server error: " + ex.Message // Provide a user-friendly error message
            });

            return context.Response.WriteAsync(result); // Send the error message as the response
        }
    }
}
