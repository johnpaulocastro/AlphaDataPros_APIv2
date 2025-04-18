using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;

namespace ADPv2.Models.Services
{
    public class EWalletService : IEWalletService
    {
        private readonly IEWalletRepository _eWalletRepository;

        public EWalletService(IEWalletRepository eWalletRepository)
        {
            this._eWalletRepository = eWalletRepository;
        }

        public async Task<EWalletEntity> GetEwallet(string customerId)
        {
            return await _eWalletRepository.GetEwallet(customerId);
        }

        public async Task<bool> UpdateEWallet(EWalletEntity entity)
        {
            return await _eWalletRepository.UpdateEWallet(entity);
        }
    }
}
