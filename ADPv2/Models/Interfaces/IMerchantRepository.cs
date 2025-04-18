using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IMerchantRepository
    {
        string GenerateMerchantId();
        Task<MerchantCompanyInfoEntity> GetMerchantCompanyInfo(string merchantId);
        Task<int> CreateMerchantInfo(MerchantInfoEntity entity);
        Task<int> CreateMerchantCompanyInfo(MerchantCompanyInfoEntity entity);
        Task<int> CreateMerchantTransactionInfo(MerchantTransactionInfoEntity entity);
        Task<int> CreateMerchantProcessingHistoryInfo(MerchantProcessingHistoryEntity entity);
        Task<int> CreateMerchantOtherInfo(MerchantOtherInfoEntity entity);
        Task<int> CreateMerchantBankInfo(MerchantBankInfoEntity entity);
    }
}
