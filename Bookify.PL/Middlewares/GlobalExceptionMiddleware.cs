using System.Net;

namespace Bookify.PL.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
                _logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);

                var isAjax = context.Request.Headers["x-requested-with"] == "XMLHttpRequest";
                if (isAjax)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
                    var message = env.IsDevelopment()
                        ? $"Unexpected error: {ex.Message}"
                        : "An unexpected error occurred. Please try again later.";

                    await context.Response.WriteAsync(message);
                }
                else
                {
                    throw; // Rethrow to let the exception handler middleware / developer page deal with it
                }
            }
        }
    }
}
