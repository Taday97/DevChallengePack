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
        public async Task InvokeAsync_ApiKeyCorrect_ShouldCallNextMiddleware()
        {
            // Arrange
            var mockNext = new Mock<RequestDelegate>();
            mockNext.Setup(m => m.Invoke(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);
            var context = new DefaultHttpContext();
            context.Request.Headers["X-API-KEY"] = "ApiKey DevChallengePackAufgabe*123";
            var middleware = new ApiKeyMiddleware(mockNext.Object);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            mockNext.Verify(m => m.Invoke(context), Times.Once);
        }
    }
}
