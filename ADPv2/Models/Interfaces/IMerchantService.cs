using ADPv2.Models.Entities;
using ADPv2.Models.ViewModels;

namespace ADPv2.Models.Interfaces
{
    public interface IMerchantService
    {
        Task<MerchantCompanyInfoEntity> GetMerchantCompanyInfo(string merchantId);

        Task<bool> CreateMerchantAccount(AccountMerchantRegistrationRequestDto request);
    }
}
