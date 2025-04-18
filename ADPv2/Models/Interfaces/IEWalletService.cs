using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IEWalletService
    {
        Task<EWalletEntity> GetEwallet(string customerId);
        Task<bool> UpdateEWallet(EWalletEntity entity);
    }
}
