using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IEWalletRepository
    {
        Task<EWalletEntity> GetEwallet(string customerId);
        Task<bool> UpdateEWallet(EWalletEntity entity);
    }
}
