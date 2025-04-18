using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace ADPv2.Models.Repositories
{
    public class CodeTransactionRepository : BaseRepository, ICodeTransactionRepository
    {
        public CodeTransactionRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<int> CreateCodeTransaction(CodeTransactionEntity entity)
        {
            var query = @"INSERT INTO tblTransactions
                                (TransactionId
                                ,PersonalInfoId
                                ,ReferenceNo
                                ,TransactionDate
                                ,PostingDate
                                ,TransactionDescription
                                ,PaymentType
                                ,DateCreated
                                ,CreatedBy
                                ,DateUpdated
                                ,UpdatedBy
                                ,Amount
                                ,TransactionStatus
                                ,ApprovalStatus
                                ,TransactionRate
                                ,TransactionCommisionRate
                                ,TransactionCompanyCommisionRate
                                ,TransactionFee
                                ,TransactionCommisionFee
                                ,TransactionCompanyCommisionFee
                                ,RollingReserve
                                ,RollingReservePeakSales)
                            VALUES (@TransactionId
                                ,@PersonalInfoId
                                ,@ReferenceNo
                                ,@TransactionDate
                                ,@PostingDate
                                ,@TransactionDescription
                                ,@PaymentType
                                ,@DateCreated
                                ,@CreatedBy
                                ,@DateUpdated
                                ,@UpdatedBy
                                ,@Amount
                                ,@TransactionStatus
                                ,@ApprovalStatus
                                ,@TransactionRate
                                ,@TransactionCommisionRate
                                ,@TransactionCompanyCommisionRate
                                ,@TransactionFee
                                ,@TransactionCommisionFee
                                ,@TransactionCompanyCommisionFee
                                ,@RollingReserve
                                ,@RollingReservePeakSales)";

            var param = new
            {
                TransactionId = entity.TransactionId,
                PersonalInfoId = entity.PersonalInfoId,
                ReferenceNo = entity.ReferenceNo,
                TransactionDate = entity.TransactionDate,
                PostingDate = entity.PostingDate,
                TransactionDescription = entity.TransactionDescription,
                PaymentType = entity.PaymentType,
                DateCreated = entity.DateCreated,
                CreatedBy = entity.CreatedBy,
                DateUpdated = entity.DateUpdated,
                UpdatedBy = entity.UpdatedBy,
                Amount = entity.Amount,
                TransactionStatus = 4,
                ApprovalStatus = 4,
                TransactionRate = entity.TransactionRate,
                TransactionCommisionRate = entity.TransactionCommisionRate,
                TransactionCompanyCommisionRate = entity.TransactionCompanyCommisionRate,
                TransactionFee = entity.TransactionFee,
                TransactionCommisionFee = entity.TransactionCommisionFee,
                TransactionCompanyCommisionFee = entity.TransactionCompanyCommisionFee,
                RollingReserve = entity.RollingReserve,
                RollingReservePeakSales = entity.RollingReservePeakSales
            };

            return await _db.ExecuteAsync(query, param);
        }
    }
}
