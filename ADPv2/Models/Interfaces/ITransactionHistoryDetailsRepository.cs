using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface ITransactionHistoryDetailsRepository
    {
        Task<int> CreateTransactionHistoryDetails(TransactionHistoryDetailsEntity entity);
        Task<List<TransactionHistoryDetailsEntity>> GetTransactionHistoryDetails(int transactionId);
    }
}
