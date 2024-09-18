using RestAPI.Services.IService;
using System.Diagnostics;

namespace RestApi.Middleware
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;

        public PerformanceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IPerformanceLoggerService logger)
        {
            var sw = new Stopwatch();
            sw.Start();

            await _next(context);

            sw.Stop();
            logger.LogPerformanceData(
                $"Request {context.Request.Method} {context.Request.Path} took about {sw.ElapsedMilliseconds} ms");
        }
    }
}
