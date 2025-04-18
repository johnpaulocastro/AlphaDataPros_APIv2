using ADPv2.Models.Entities;
using ADPv2.Models.ViewModels;

namespace ADPv2.Models.Interfaces
{
    public interface ITransactionHistoryService
    {
        Task<List<TransactionHistoryEntity>> GetTransactionList();
        Task<TransactionHistoryEntity> GetTransaction(int transactionNo);
        Task<TransactionHistoryEntity> GetTransaction(string orderNumber);
        Task<TransactionHistoryCreateResponseDto?> CreateTransaction(TransactionHistoryRequestDto requestDto, string userLogged, bool isCreditCard = false);
    }
}
