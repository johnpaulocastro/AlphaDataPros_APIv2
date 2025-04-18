using ADPv2.Models.ViewModels;

namespace ADPv2.Models.Interfaces
{
    public interface IEWalletTransactionService
    {
        Task<int> CreateEWalletTransaction(EWalletTransactionRequestDto entity);
    }
}
