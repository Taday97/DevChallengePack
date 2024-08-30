using RestApi.Models;
using RestAPI.Repositories.IRepository;

namespace RestAPI.Repositories.Repository
{
    public class PerformanceLoggerService : IPerformanceLoggerService
    {
        List<string> _logger = new List<string>();

        public PerformanceLoggerService()
        {
            _logger = _logger ?? new List<string>();
        }

        public void LogPerformanceData(string message)
        {
            _logger.Add(message);
        }
    }
}