using SearchScopeAPI.SerachScope.API.Logger;
using System.Net;

namespace SearchScopeAPI.SerachScope.API.Middleware
{
    /// <summary>
    /// GlobalExceptionHandlerMiddleware class.
    /// </summary>
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CustomLogger _customLogger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="next">Specify RequestDelegate.</param>
        /// <param name="customLogger">Specify custom logger.</param>
        public GlobalExceptionHandlerMiddleware(RequestDelegate next, CustomLogger customLogger)
        {
            _next = next;
            _customLogger = customLogger;
        }

        /// <summary>
        /// To register this GlobalExceptionHandlerMiddleware in the http request pipeline.
        /// </summary>
        /// <param name="context">Specify HttpContext.</param>
        /// <returns>Task.</returns>
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

        /// <summary>
        /// To handle exception globally.
        /// </summary>
        /// <param name="context">Specify HttpContext.</param>
        /// <param name="exception">Specify exception.</param>
        /// <returns>Task.</returns>
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
