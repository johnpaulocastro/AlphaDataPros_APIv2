using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Models.Repositories;

namespace ADPv2.Models.Services
{
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly IPersonalInfoRepository _personalInfoRepository;

        public PersonalInfoService(IPersonalInfoRepository _personalInfoRepository)
        {
            this._personalInfoRepository = _personalInfoRepository;
        }

        public async Task<PersonalInfoEntity> GetPersonalInfo(string customerId)
        {
            return await _personalInfoRepository.GetPersonalInfo(customerId);
        }
    }
}
