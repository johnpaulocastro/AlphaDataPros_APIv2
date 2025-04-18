using ADPv2.Logger.Model;

namespace ADPv2.Logger.Interface
{
    public interface IRequestResponseLogModelCreator
    {
        RequestResponseLogModel LogModel { get; }
        Task<string> LogString();
    }
}
