namespace ADPv2.Models.Interfaces
{
    public interface IStatusService
    {
        Task<string> GetDescription(int statusId);
    }
}
