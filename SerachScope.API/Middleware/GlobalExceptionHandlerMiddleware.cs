using Microsoft.AspNetCore.Mvc;

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
                _customLogger.LogError(ex.ToString());
                await HandleGlobalException(context, ex);
                return;
            }
        }

        private async Task HandleGlobalException(HttpContext context, Exception exception)
        {
            ProblemDetails problemDetails = null;
            try
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var problemDetailsFactory = context.RequestServices.GetRequiredService<Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory>();
                problemDetails = problemDetailsFactory.CreateProblemDetails(context, StatusCodes.Status500InternalServerError, detail: "Internal Server Error");

                if (exception != null)
                {
                    problemDetails.Detail = exception.Message;
                }
                else
                {
                    problemDetails.Detail = @"An unexpected error occured.";
                }

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (Exception e)
            {
                _customLogger.LogError(e.ToString());
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
