using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Models.ViewModels;

namespace ADPv2.Models.Services
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        private readonly IUserBuyRateRepository _userBuyRateRepository;

        public TransactionHistoryService(
            ITransactionHistoryRepository transactionHistoryRepository,
            IUserBuyRateRepository _userBuyRateRepository
            )
        {
            this._transactionHistoryRepository = transactionHistoryRepository;
            this._userBuyRateRepository = _userBuyRateRepository;
        }

        public async Task<TransactionHistoryCreateResponseDto?> CreateTransaction(TransactionHistoryRequestDto requestDto, string userLogged, bool isCreditCard = false)
        {
            var transactionNo = await _transactionHistoryRepository.GenerateTransactionNo();

            // All create transaction are by far default "Pass"
            var approvalStatusId = 9;
            var transactionStatusId = 9;

            var userBuyRate = await _userBuyRateRepository.GetUserBuyRate(userLogged);
            if (userBuyRate == null) return null;

            var decTransactionRate = userBuyRate.TransactionRate / 100;
            var decCreditCardTransactionRate = userBuyRate.CreditCardTransactionRate / 100;
            var decTransactionCommisionRate = userBuyRate.TransactionCommisionRate / 100;
            var decTransactionCompanyCommisionRate = userBuyRate.TransactionCompanyCommisionRate / 100;
            var decTransactionFee = userBuyRate.TransactionFee;
            var decCreditCardTransactionFee = userBuyRate.CreditCardTransactionFee;
            var decRollingReserve = userBuyRate.RollingReserve / 100;
            var decRollingReservePeakSales = userBuyRate.RollingReservePeakSales / 100;
            var decTransactionCommisionFee = userBuyRate.TransactionCommisionFee;
            var decTransactionCompanyCommisionFee = userBuyRate.TransactionCompanyCommisionFee;

            var amount = Convert.ToDecimal(requestDto.Amount);
            var entity = new TransactionHistoryEntity
            {
                TransactionNo = transactionNo,
                TransactionApprovalStatusID = approvalStatusId,
                TransactionStatusID = transactionStatusId,
                CustomerFirstName = requestDto.FirstName,
                CustomerLastName = requestDto.LastName,
                CustomerPhoneNumber = requestDto.PhoneNumber,
                CustomerStreetNumber = requestDto.StreetNumber,
                CustomerStreetUnitNumber = requestDto.UnitNumber,
                CustomerStreetName = requestDto.StreetName,
                CustomerCity = requestDto.City,
                CustomerState = requestDto.State,
                CustomerZipCode = requestDto.ZipCode,
                CustomerEmailAddress = requestDto.EmailAddress,
                BankName = requestDto.BankName,
                BankRoutingNumber = requestDto.RoutingNumber,
                BankAccountNumber = requestDto.AccountNumber,
                Amount = amount,
                CheckNo = requestDto.CheckNo,
                MerchantId = userLogged,

                TransactionRate = !isCreditCard ? decTransactionRate * amount : 0,
                CreditCardTransactionRate = isCreditCard ? decCreditCardTransactionRate * amount : 0,

                TransactionCommissionRate = decTransactionCommisionRate * amount,
                TransactionCompanyCommissionRate = decTransactionCompanyCommisionRate * amount,

                TransactionFee = !isCreditCard ? decTransactionFee : 0,
                CreditCardTransactionFee = isCreditCard ? decCreditCardTransactionFee : 0,

                RollingReserve = decRollingReserve * amount,
                RollingReservePeakSales = decRollingReservePeakSales * amount,
                TransactionCommissionFee = decTransactionCommisionFee,
                TransactionCompanyCommissionFee = decTransactionCompanyCommisionFee,

                CreatedBy = userLogged,
                DateCreated = DateTime.Now
            };
            var isSuccess = await _transactionHistoryRepository.CreateTransactionHistory(entity);

            if (isSuccess)
            {
                var response = new TransactionHistoryCreateResponseDto
                {
                    TransactionId = Convert.ToInt32(transactionNo),
                    ApprovalStatusId = approvalStatusId,
                    TransactionStatusId = transactionStatusId,
                };
                return response;
            }

            return null;
        }

        public async Task<List<TransactionHistoryEntity>> GetTransactionList()
        {
            var result = await _transactionHistoryRepository.GetTransactionHistoryList();
            return result;
        }

        public async Task<TransactionHistoryEntity> GetTransaction(int transactionNo)
        {
            var result = await _transactionHistoryRepository.GetTransactionHistory(transactionNo);
            return result;
        }

        public async Task<TransactionHistoryEntity> GetTransaction(string orderNumber)
        {
            var result = await _transactionHistoryRepository.GetTransactionHistory(orderNumber);
            return result;
        }
    }
}
