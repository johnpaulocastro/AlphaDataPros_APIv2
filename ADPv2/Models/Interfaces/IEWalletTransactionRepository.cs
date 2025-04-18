using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IEWalletTransactionRepository
    {
        Task<int> CreateEWalletTransaction(EWalletTransactionEntity entity);
    }
}
