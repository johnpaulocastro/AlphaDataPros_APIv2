using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface ITransactionHistoryRepository
    {
        Task<string> GenerateTransactionNo();
        Task<List<TransactionHistoryEntity>> GetTransactionHistoryList();
        Task<TransactionHistoryEntity> GetTransactionHistory(int transactionId);
        Task<TransactionHistoryEntity> GetTransactionHistory(string orderNumber);
        Task<bool> CreateTransactionHistory(TransactionHistoryEntity entity);
    }
}
