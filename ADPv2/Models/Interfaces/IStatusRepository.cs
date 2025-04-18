using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IStatusRepository
    {
        Task<StatusEntity> GetStatus(int statusId);
    }
}
