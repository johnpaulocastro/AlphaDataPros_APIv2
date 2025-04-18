using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace ADPv2.Models.Repositories
{
    public class TransactionHistoryRepository : BaseRepository, ITransactionHistoryRepository
    {
        public TransactionHistoryRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<string> GenerateTransactionNo()
        {
            var query = @"SELECT
                            COUNT(ID) + 1 AS MaxID
                          FROM tblC2PTransactionHistory";

            var result = await _db.ExecuteScalarAsync<int>(query);
            return result.ToString();
        }

        public async Task<List<TransactionHistoryEntity>> GetTransactionHistoryList()
        {
            var query = "SELECT TOP 50 * FROM tblC2PTransactionHistory";
            var result = await _db.QueryAsync<TransactionHistoryEntity>(query);

            return result.ToList();
        }

        public async Task<TransactionHistoryEntity> GetTransactionHistory(int transactionId)
        {
            var query = "SELECT * FROM tblC2PTransactionHistory WHERE TransactionNo = @TransactionNo";
            var parameters = new
            {
                TransactionNo = transactionId,
            };

            var result = await _db.QuerySingleOrDefaultAsync<TransactionHistoryEntity>(query, parameters);
            return result;
        }

        public async Task<TransactionHistoryEntity> GetTransactionHistory(string orderNumber)
        {
            var query = "SELECT * FROM tblC2PTransactionHistory WHERE TransactionNotes LIKE @OrderNumber";
            var parameters = new
            {
                OrderNumber = $"%{orderNumber}%",
            };

            var result = await _db.QuerySingleOrDefaultAsync<TransactionHistoryEntity>(query, parameters);
            return result;
        }

        public async Task<bool> CreateTransactionHistory(TransactionHistoryEntity entity)
        {
            var query = $@"INSERT INTO [dbo].[tblC2PTransactionHistory]
                               ([TransactionNo]
                               ,[TransactionApprovalStatusID]
                               ,[TransactionStatusID]
                               ,[CustomerFirstName]
                               ,[CustomerLastName]
                               ,[CustomerPhoneNumber]
                               ,[CustomerStreetNumber]
                               ,[CustomerStreetUnitNumber]
                               ,[CustomerStreetName]
                               ,[CustomerCity]
                               ,[CustomerState]
                               ,[CustomerZipCode]
                               ,[CustomerEmailAddress]
                               ,[BankName]
                               ,[BankRoutingNumber]
                               ,[BankAccountNumber]
                               ,[Amount]
                               ,[CheckNo]
                               ,[TransactionRate]
                               ,[TransactionRateCreditCard]
                               ,[TransactionCommissionRate]
                               ,[TransactionCompanyCommissionRate]
                               ,[TransactionFee]
                               ,[CreditCardTransactionFee]
                               ,[TransactionCommissionFee]
                               ,[TransactionCompanyCommissionFee]
                               ,[RollingReserve]
                               ,[RollingReservePeakSales]
                               ,[TransactionNotes]
                               ,[MerchantId]
                               ,[PersonalInfoId]
                               ,[CheckingAccountID]
                               ,[TransactedBank]
                               ,[DatePrintQue]
                               ,[CreatedBy]
                               ,[DateCreated])
                         VALUES
                               (@TransactionNo
                               ,@TransactionApprovalStatusID
                               ,@TransactionStatusID
                               ,@CustomerFirstName
                               ,@CustomerLastName
                               ,@CustomerPhoneNumber
                               ,@CustomerStreetNumber
                               ,@CustomerStreetUnitNumber
                               ,@CustomerStreetName
                               ,@CustomerCity
                               ,@CustomerState
                               ,@CustomerZipCode
                               ,@CustomerEmailAddress
                               ,@BankName
                               ,@BankRoutingNumber
                               ,@BankAccountNumber
                               ,@Amount
                               ,@CheckNo
                               ,@TransactionRate
                               ,@TransactionRateCreditCard
                               ,@TransactionCommissionRate
                               ,@TransactionCompanyCommissionRate
                               ,@TransactionFee
                               ,@CreditCardTransactionFee
                               ,@TransactionCommissionFee
                               ,@TransactionCompanyCommissionFee
                               ,@RollingReserve
                               ,@RollingReservePeakSales
                               ,@TransactionNotes
                               ,@MerchantId
                               ,@PersonalInfoId
                               ,@CheckingAccountID
                               ,@TransactedBank
                               ,@DatePrintQue
                               ,@CreatedBy
                               ,@DateCreated)";

            var parameters = new
            {
                TransactionNo = entity.TransactionNo,
                TransactionApprovalStatusID = entity.TransactionApprovalStatusID,
                TransactionStatusID = entity.TransactionStatusID,
                CustomerFirstName = entity.CustomerFirstName,
                CustomerLastName = entity.CustomerLastName,
                CustomerPhoneNumber = entity.CustomerPhoneNumber,
                CustomerStreetNumber = entity.CustomerStreetNumber,
                CustomerStreetUnitNumber = entity.CustomerStreetUnitNumber,
                CustomerStreetName = entity.CustomerStreetName,
                CustomerCity = entity.CustomerCity,
                CustomerState = entity.CustomerState,
                CustomerZipCode = entity.CustomerZipCode,
                CustomerEmailAddress = entity.CustomerEmailAddress,
                BankName = entity.BankName,
                BankRoutingNumber = entity.BankRoutingNumber,
                BankAccountNumber = entity.BankAccountNumber,
                Amount = entity.Amount,
                CheckNo = entity.CheckNo,
                TransactionRate = entity.TransactionRate,
                TransactionRateCreditCard = entity.CreditCardTransactionRate,
                TransactionCommissionRate = entity.TransactionCommissionRate,
                TransactionCompanyCommissionRate = entity.TransactionCompanyCommissionRate,
                TransactionFee = entity.TransactionFee,
                CreditCardTransactionFee = entity.CreditCardTransactionFee,
                TransactionCommissionFee = entity.TransactionCommissionFee,
                TransactionCompanyCommissionFee = entity.TransactionCompanyCommissionFee,
                RollingReserve = entity.RollingReserve,
                RollingReservePeakSales = entity.RollingReservePeakSales,
                TransactionNotes = entity.TransactionNotes,
                MerchantId = entity.MerchantId,
                PersonalInfoId = entity.PersonalInfoId,
                CheckingAccountID = entity.CheckingAccountID,
                TransactedBank = entity.TransactedBank,
                DatePrintQue = entity.DatePrintQue,
                CreatedBy = entity.CreatedBy,
                DateCreated = entity.DateCreated
            };

            var result = await _db.ExecuteAsync(query, parameters);
            return (result > 0);
        }
    }
}
