using ADPv2.Logger.Interface;
using ADPv2.Logger.Model;
using ADPv2.Models.Interfaces;
using Newtonsoft.Json;

namespace ADPv2.Logger.Service
{
    public class RequestResponseLogModelCreator : IRequestResponseLogModelCreator
    {
        public RequestResponseLogModel LogModel { get; private set; }
        private readonly IRequestResponseLoggerService _service;

        public RequestResponseLogModelCreator(IRequestResponseLoggerService service)
        {
            LogModel = new RequestResponseLogModel();
            _service = service;
        }

        public async Task<string> LogString()
        {
            await _service.CreateRequestResponseLogger(LogModel);
            var jsonString = JsonConvert.SerializeObject(LogModel);
            return jsonString;
        }
    }
}
