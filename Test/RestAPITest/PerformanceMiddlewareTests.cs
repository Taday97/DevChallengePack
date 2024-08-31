using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using RestAPI.Repositories.Repository;
using Moq;
using RestApi.Middleware;
using RestAPI.Repositories.IRepository;
using System.Text.RegularExpressions;

namespace Test.RestAPITest
{
    public class PerformanceMiddlewareTests
    {
        [Fact]
        public async Task Invoke_LogsCorrectRequestDuration()
        {
            // Arrange
            var loggerMock = new Mock<IPerformanceLoggerService>();
            var context = new DefaultHttpContext();
            context.Request.Method = "GET";
            context.Request.Path = "/api/WorkstationBooking";

            // Simulate a request delegate that introduces a delay
            RequestDelegate next = async (innerHttpContext) =>
            {
                await Task.Delay(100); // Simulate 100 ms processing
            };

            var middleware = new PerformanceMiddleware(next);

            // Act
            await middleware.InvokeAsync(context, loggerMock.Object);

            // Assert
            loggerMock.Verify(
                x => x.LogPerformanceData(It.Is<string>(s => s.Contains("Request GET /api/WorkstationBooking took about"))),
                Times.Once);

            // Additional verification: extract and validate the duration from the log message
            loggerMock.Verify(
                x => x.LogPerformanceData(It.Is<string>(s => ExtractDurationFromMessage(s))),
                Times.Once);
        }

        // Helper method to extract the duration from the log message
        private static bool ExtractDurationFromMessage(string message)
        {
            // This example assumes the message contains "took <duration> ms"
            var match = Regex.Match(message, @"took about (\d+) ms");
            if (match.Success && long.TryParse(match.Groups[1].Value, out var duration))
            {
                return duration>0;
            }
            return false;
        }

    }


}
