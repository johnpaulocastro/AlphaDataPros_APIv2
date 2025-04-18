using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Microsoft.Extensions.Options;
using static Dapper.SqlMapper;

namespace ADPv2.Models.Repositories
{
    public class TransactionHistoryDetailsRepository : BaseRepository, ITransactionHistoryDetailsRepository
    {
        public TransactionHistoryDetailsRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<List<TransactionHistoryDetailsEntity>> GetTransactionHistoryDetails(int transactionId)
        {
            var query = "SELECT * FROM [dbo].[tblC2PTransactionHistoryDetails] WHERE TransactionIdNo = @TransactionId AND TransactionNotes <> '' AND IsDeleted = 0";

            var param = new
            {
                TransactionId = transactionId,
            };

            var result = await _db.QueryAsync<TransactionHistoryDetailsEntity>(query, param);
            return result.ToList();
        }

        public async Task<int> CreateTransactionHistoryDetails(TransactionHistoryDetailsEntity entity)
        {
            var query = @"INSERT INTO [dbo].[tblC2PTransactionHistoryDetails]
                               ([TransactionIdNo]
                               ,[TransactionApprovalStatusID]
                               ,[TransactionStatusID]
                               ,[TransactionTypeID]
                               ,[ReturnCodeID]
                               ,[TransactionNotes]
                               ,[IsDeleted]
                               ,[CreatedBy]
                               ,[DateCreated]
                               ,[UpdatedBy]
                               ,[DateUpdated])
                         VALUES
                               (@TransactionIdNo
                               ,@TransactionApprovalStatusID
                               ,@TransactionStatusID
                               ,@TransactionTypeID
                               ,@ReturnCodeID
                               ,@TransactionNotes
                               ,@IsDeleted
                               ,@CreatedBy
                               ,@DateCreated
                               ,@UpdatedBy
                               ,@DateUpdated)";

            var parameters = new
            {
                TransactionIdNo = entity.TransactionIdNo,
                TransactionApprovalStatusID = entity.TransactionApprovalStatusID,
                TransactionStatusID = entity.TransactionStatusID,
                TransactionTypeID = entity.TransactionTypeID,
                ReturnCodeID = entity.ReturnCodeID,
                TransactionNotes = entity.TransactionNotes,
                IsDeleted = entity.IsDeleted,
                CreatedBy = entity.CreatedBy,
                DateCreated = entity.DateCreated,
                UpdatedBy = entity.UpdatedBy,
                DateUpdated = entity.DateUpdated
            };

            var result = await _db.ExecuteAsync(query, parameters);
            return result;
        }
    }
}
