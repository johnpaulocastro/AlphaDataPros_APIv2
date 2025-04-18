using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IUserBuyRateRepository
    {
        Task<UsersBuyRatesEntity> GetUserBuyRate(string merchantId);
    }
}
