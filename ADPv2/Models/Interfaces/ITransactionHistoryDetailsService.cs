using ADPv2.Models.Entities;
using ADPv2.Models.ViewModels;

namespace ADPv2.Models.Interfaces
{
    public interface ITransactionHistoryDetailsService
    {
        Task<List<TransactionHistoryDetailsRemarksResponseDto>> GetTransactionHistoryDetails(int transactionId);
        Task<bool> CreateInitialTransactionHistoryDetails(TransactionHistoryCreateRequestDto objModel, bool isApi = false);
    }
}
