using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IPersonalInfoService
    {
        Task<PersonalInfoEntity> GetPersonalInfo(string customerId);
    }
}
