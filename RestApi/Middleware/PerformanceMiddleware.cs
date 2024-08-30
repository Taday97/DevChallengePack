using RestAPI.Repositories.IRepository;
using System.Diagnostics;

namespace RestApi.Middleware
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IPerformanceLoggerService _logger;

        public PerformanceMiddleware(RequestDelegate next, IPerformanceLoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();

            await _next(context);

            sw.Stop();
            _logger.LogPerformanceData(
                $"Request {context.Request.Method} {context.Request.Path} took about {sw.ElapsedMilliseconds} ms");
        }
    }
}
