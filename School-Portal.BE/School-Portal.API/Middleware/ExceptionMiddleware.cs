using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using School_Portal.BE.Core.Application.Models.DTOs;
using System.Text.Json;

namespace School_Portal.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                logger.LogError($"An error occured: {exception.Message} executing {context.Request.Path.Value}. \n Details: {JsonSerializer.Serialize(new Error(exception))}");

                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                await response.WriteAsync(JsonSerializer.Serialize(new { status = false, message = "An error occured perfoming the operation" }));
            }
        }
    }

    public class Error
    {
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public Error()
        {
            this.TimeStamp = DateTime.Now;
        }

        public Error(string Message) : this()
        {
            this.Message = Message;
        }

        public Error(System.Exception ex) : this(ex.Message)
        {
            this.StackTrace = ex.StackTrace;
        }

        public override string ToString()
        {
            return this.Message + this.StackTrace;
        }
    }
}
