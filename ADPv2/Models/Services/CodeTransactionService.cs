using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Models.ViewModels;

namespace ADPv2.Models.Services
{
    public class CodeTransactionService : ICodeTransactionService
    {
        private readonly ICodeTransactionRepository _codeTransactionRepository;
        private readonly IUserBuyRateRepository _userBuyRateRepository;

        public CodeTransactionService(
            ICodeTransactionRepository codeTransactionRepository,
            IUserBuyRateRepository userBuyRateRepository)
        {
            this._codeTransactionRepository = codeTransactionRepository;
            this._userBuyRateRepository = userBuyRateRepository;
        }

        public async Task<string> CreateCodeTransaction(TransactionHistoryAlphaCodeRequestDto request, string customerId, string transactionDescription, string userLogged)
        {
            var userBuyRate = await _userBuyRateRepository.GetUserBuyRate(userLogged);
            if (userBuyRate == null) return null;

            var decTransactionRate = userBuyRate.TransactionRate / 100;
            var decTransactionCommisionRate = userBuyRate.TransactionCommisionRate / 100;
            var decTransactionCompanyCommisionRate = userBuyRate.TransactionCompanyCommisionRate / 100;
            var decTransactionFee = userBuyRate.TransactionFee;
            var decRollingReserve = userBuyRate.RollingReserve / 100;
            var decRollingReservePeakSales = userBuyRate.RollingReservePeakSales / 100;
            var decTransactionCommisionFee = userBuyRate.TransactionCommisionFee;
            var decTransactionCompanyCommisionFee = userBuyRate.TransactionCompanyCommisionFee;

            var transactionId = DateTime.Now.ToString("yyyyMMddhhmmss");

            var codeTransaction = new CodeTransactionEntity
            {
                TransactionId = transactionId,
                PersonalInfoId = customerId,
                ReferenceNo = $"REF{transactionId}",
                TransactionDate = DateTime.Now.Date,
                PostingDate = DateTime.Now.Date,
                TransactionDescription = transactionDescription,
                PaymentType = "EWallet",
                Amount = request.Amount,

                TransactionRate = decTransactionRate * request.Amount,
                TransactionCommisionRate = decTransactionCommisionRate * request.Amount,
                TransactionCompanyCommisionRate = decTransactionCompanyCommisionRate * request.Amount,
                TransactionFee = decTransactionFee * request.Amount,
                RollingReserve = decRollingReserve * request.Amount,
                RollingReservePeakSales = decRollingReservePeakSales * request.Amount,
                TransactionCommisionFee = decTransactionCommisionFee * request.Amount,
                TransactionCompanyCommisionFee = decTransactionCompanyCommisionFee * request.Amount,

                DateCreated = DateTime.Now,
                CreatedBy = userLogged,
                DateUpdated = DateTime.Now,
                UpdatedBy = userLogged
            };

            var result = await _codeTransactionRepository.CreateCodeTransaction(codeTransaction);
            if (result > 0) return transactionId;
            
            return null;
        }
    }
}
