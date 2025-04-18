using ADPv2.Models.Entities;
using ADPv2.Models.ViewModels;

namespace ADPv2.Models.Interfaces
{
    public interface ICodeTransactionService
    {
        Task<string> CreateCodeTransaction(TransactionHistoryAlphaCodeRequestDto request, string customerId, string transactionDescription, string userLogged);
    }
}
