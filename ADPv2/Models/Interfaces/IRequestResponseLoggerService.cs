using ADPv2.Logger.Model;

namespace ADPv2.Models.Interfaces
{
    public interface IRequestResponseLoggerService
    {
        Task CreateRequestResponseLogger(RequestResponseLogModel request);
    }
}
