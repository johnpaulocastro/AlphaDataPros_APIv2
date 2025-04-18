using ADPv2.Logger.Model;
using ADPv2.Models.Interfaces;

namespace ADPv2.Models.Services
{
    public class RequestResponseLoggerService : IRequestResponseLoggerService
    {
        private readonly IRequestResponseLoggerRepository _loggerRepository;

        public RequestResponseLoggerService(IRequestResponseLoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }

        public async Task CreateRequestResponseLogger(RequestResponseLogModel request)
        {
            await _loggerRepository.CreateRequestResponseLogger(request);
        }
    }
}
