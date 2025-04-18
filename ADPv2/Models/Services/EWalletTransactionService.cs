using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Models.ViewModels;

namespace ADPv2.Models.Services
{
    public class EWalletTransactionService : IEWalletTransactionService
    {
        private readonly IEWalletTransactionRepository _eWalletTransactionRepository;

        public EWalletTransactionService(IEWalletTransactionRepository _eWalletTransactionRepository)
        {
            this._eWalletTransactionRepository = _eWalletTransactionRepository;
        }

        public async Task<int> CreateEWalletTransaction(EWalletTransactionRequestDto request)
        {
            var entity = new EWalletTransactionEntity
            {
                PersonalInfoId = request.CustomerId,
                MerchantId = request.MerchantId,
                TransactionId = request.TransactionId,
                TransactionDate = DateTime.Now,
                TransactionDescription = request.TransactionDescription,
                Note = $"Payment for {request.TransactionDescription}",
                Amount = request.Amount,
                DateCreated = DateTime.Now,
                CreatedBy = request.MerchantId,
                DateUpdated = DateTime.Now,
                UpdatedBy = request.MerchantId
            };

            return await _eWalletTransactionRepository.CreateEWalletTransaction(entity);
        }
    }
}
