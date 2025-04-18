using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace ADPv2.Models.Repositories
{
    public class EWalletTransactionRepository : BaseRepository, IEWalletTransactionRepository
    {
        public EWalletTransactionRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<int> CreateEWalletTransaction(EWalletTransactionEntity entity)
        {
            var query = @"INSERT INTO tblEWalletTransaction (
                            PersonalInfoId,
                            MerchantId,
                            TransactionId,
                            TransactionDate,
                            TransactionDescription,
                            Note,
                            Amount,
                            DateCreated,
                            CreatedBy,
                            DateUpdated,
                            UpdatedBy
                          ) VALUES (
                            @PersonalInfoId,
                            @MerchantId,
                            @TransactionId,
                            @TransactionDate,
                            @TransactionDescription,
                            @Note,
                            @Amount,
                            @DateCreated,
                            @CreatedBy,
                            @DateUpdated,
                            @UpdatedBy
                          )";

            var param = new
            {
                PersonalInfoId = entity.PersonalInfoId,
                MerchantId = entity.MerchantId,
                TransactionId = entity.TransactionId,
                TransactionDate = entity.TransactionDate,
                TransactionDescription = entity.TransactionDescription,
                Note = entity.Note,
                Amount = entity.Amount,
                DateCreated = entity.DateCreated,
                CreatedBy = entity.CreatedBy,
                DateUpdated = entity.DateUpdated,
                UpdatedBy = entity.UpdatedBy
            };

            return await _db.ExecuteAsync(query, param);
        }

    }
}
