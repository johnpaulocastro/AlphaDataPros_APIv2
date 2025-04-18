using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface ICreditCardAccountNumberRepository
    {
        Task<CreditCardAccountNumberEntity> GetCreditCardAccountNumberById(int id);
        Task<List<CreditCardAccountNumberEntity>> GetCreditCardAccountNumberByMerchantId(string merchantId);
        Task<CreditCardAccountNumberEntity> GetCreditCardAccountNumberByAccountNumber(int accountNumber);
        Task<CreditCardAccountNumberEntity> GetCreditCardAccountNumberByRoutingNumber(int routingNumber);
        Task<int> CreateCreditCardAccountNumber(CreditCardAccountNumberEntity entity);
        Task<bool> UpdateCreditCardAccountNumber(CreditCardAccountNumberEntity entity);
    }
}
