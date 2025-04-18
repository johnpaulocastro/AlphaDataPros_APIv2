using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;

namespace ADPv2.Models.Services
{
    public class CreditCardAccountNumberService : ICreditCardAccountNumberService
    {
        private readonly ICreditCardAccountNumberRepository _creditCardAccountNumberRepository;

        public CreditCardAccountNumberService(ICreditCardAccountNumberRepository creditCardAccountNumberRepository)
        {
            _creditCardAccountNumberRepository = creditCardAccountNumberRepository;
        }

        public async Task<CreditCardAccountNumberEntity> GetCreditCardAccountNumberById(int id)
        {
            return await _creditCardAccountNumberRepository.GetCreditCardAccountNumberById(id);
        }

        public async Task<List<CreditCardAccountNumberEntity>> GetCreditCardAccountNumberByMerchantId(string merchantId)
        {
            return await _creditCardAccountNumberRepository.GetCreditCardAccountNumberByMerchantId(merchantId);
        }

        public async Task<CreditCardAccountNumberEntity> GetCreditCardAccountNumberByAccountNumber(int accountNumber)
        {
            return await _creditCardAccountNumberRepository.GetCreditCardAccountNumberByAccountNumber(accountNumber);
        }

        public async Task<CreditCardAccountNumberEntity> GetCreditCardAccountNumberByRoutingNumber(int routingNumber)
        {
            return await _creditCardAccountNumberRepository.GetCreditCardAccountNumberByRoutingNumber(routingNumber);
        }

        public async Task<int> CreateCreditCardAccountNumber(CreditCardAccountNumberEntity entity)
        {
            return await _creditCardAccountNumberRepository.CreateCreditCardAccountNumber(entity);
        }

        public async Task<bool> UpdateCreditCardAccountNumber(CreditCardAccountNumberEntity entity)
        {
            return await _creditCardAccountNumberRepository.UpdateCreditCardAccountNumber(entity);
        }
    }
}
