using RestApi.Models;
using RestAPI.Services.IService;

namespace RestAPI.Services.Service
{
    public class PerformanceLoggerService : IPerformanceLoggerService
    {
        private readonly List<string> _logger = new List<string>();

        public void LogPerformanceData(string message)
        {
            _logger.Add(message);
        }
    }
}