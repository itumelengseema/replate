namespace Replate.Api.Middleware
{
    public class GlobalExceptionHandllingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandllingMiddleware> _logger;
        public GlobalExceptionHandllingMiddleware(RequestDelegate next,
            ILogger<GlobalExceptionHandllingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception (logging logic not shown here)
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred.");
                _logger.LogError(ex, "An unexpected error occurred.");
            }
        }
    }
}
