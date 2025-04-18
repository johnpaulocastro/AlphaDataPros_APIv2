using ADPv2.Logger.Model;

namespace ADPv2.Models.Interfaces
{
    public interface IRequestResponseLoggerRepository
    {
        Task CreateRequestResponseLogger(RequestResponseLogModel request);
    }
}
