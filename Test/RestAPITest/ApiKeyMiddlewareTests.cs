using Microsoft.AspNetCore.Http;
using Moq;
using RestApi.Middleware;
using System.Threading.Tasks;
using Xunit;
using System.Text;
using ConsoleAPP;

namespace Test.RestAPITest
{
    public class ApiKeyMiddlewareTests
    {
    [Fact]
        public async Task InvokeAsync_ApiKeyNotPresent_ShouldReturn401()
        {
            // Arrange
            var mockNext = new Mock<RequestDelegate>();
            var context = new DefaultHttpContext();
            var middleware = new ApiKeyMiddleware(mockNext.Object);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(401, context.Response.StatusCode);
        }

        [Fact]
        public async Task InvokeAsync_ApiKeyIncorrect_ShouldReturn401()
        {
            // Arrange
            var mockNext = new Mock<RequestDelegate>();
            var context = new DefaultHttpContext();
            context.Request.Headers["X-API-KEY"] = "ApiKey wrongkey";
            var middleware = new ApiKeyMiddleware(mockNext.Object);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(401, context.Response.StatusCode);
        }
        [Fact]
        public async Task InvokeAsync_ShouldReturnUnauthorized_WhenApiKeyIsMissing()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            var next = new Mock<RequestDelegate>();
            var middleware = new ApiKeyMiddleware(next.Object);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(401, context.Response.StatusCode); // Unauthorized
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            Assert.Equal("API Key not found", responseText);
        }

        [Fact]
        public async Task InvokeAsync_ApiKeyCorrect_ShouldCallNextMiddleware()
        {
            // Arrange
            var mockNext = new Mock<RequestDelegate>();
            mockNext.Setup(m => m.Invoke(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);
            var context = new DefaultHttpContext();
            context.Request.Headers["X-API-KEY"] = "ApiKey GETECAufgabe*123";
            var middleware = new ApiKeyMiddleware(mockNext.Object);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            mockNext.Verify(m => m.Invoke(context), Times.Once);
        }
    }
}
