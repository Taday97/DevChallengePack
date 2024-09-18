namespace RestApi.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "X-API-KEY";
        private const string ApiKeyPrefix = "ApiKey ";
        private const string configuredApiKey = "DevChallengePackAufgabe*123";
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("API Key not found");
                return;
            }

            if (!extractedApiKey.ToString().StartsWith(ApiKeyPrefix) ||
                extractedApiKey.ToString().Length <= ApiKeyPrefix.Length)
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid API Key format");
                return;
            }

            var actualApiKey = extractedApiKey.ToString().Substring(ApiKeyPrefix.Length);

           
            if (!configuredApiKey.Equals(actualApiKey))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            await _next(context);
        }
    }
}