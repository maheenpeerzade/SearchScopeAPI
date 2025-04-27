using SearchScopeAPI.SerachScope.API.Logger;
using System.Net;

namespace SearchScopeAPI.SerachScope.API.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CustomLogger _customLogger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, CustomLogger customLogger)
        {
            _next = next;
            _customLogger = customLogger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _customLogger.LogError(ex);
                await HandleGlobalExceptionAsync(context, ex);
                return;
            }
        }

        private async Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // Prepare response body for the client
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unhandled exception has occurred.",
                Detail = exception?.Message, // Include exception details if necessary
                Path = context.Request.Path // The endpoint path where the exception occurred
            };

            // Write the response as JSON
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
