using ADPv2.Logger.Interface;

namespace ADPv2.Logger.Class
{
    public class RequestResponseLogger : IRequestResponseLogger
    {
        private readonly ILogger<RequestResponseLogger> _logger;

        public RequestResponseLogger(ILogger<RequestResponseLogger> logger)
        {
            _logger = logger;
        }

        public async void Log(IRequestResponseLogModelCreator logCreator)
        {
            var _logCreator = await logCreator.LogString();
            _logger.LogCritical(_logCreator);
        }
    }
}
