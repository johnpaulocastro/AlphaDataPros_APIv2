using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;

namespace ADPv2.Models.Services
{
    public class UserBuyRateService : IUserBuyRateService
    {
        private readonly IUserBuyRateRepository _userBuyRateRepository;

        public UserBuyRateService(IUserBuyRateRepository _userBuyRateRepository)
        {
            this._userBuyRateRepository = _userBuyRateRepository;
        }

        public async Task<UsersBuyRatesEntity> GetUserBuyRate(string merchantId)
        {
            return await _userBuyRateRepository.GetUserBuyRate(merchantId);
        }
    }
}
