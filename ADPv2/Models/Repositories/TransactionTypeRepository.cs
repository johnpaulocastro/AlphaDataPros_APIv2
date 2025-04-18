using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace ADPv2.Models.Repositories
{
    public class TransactionTypeRepository : BaseRepository, ITransactionTypeRepository
    {
        public TransactionTypeRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<TransactionTypeEntity> GetTransactionTypeById(int id)
        {
            var param = new { Id = id };
            var query = @"SELECT TransactionTypeID
                              ,TransactionTypeDescription
                              ,CreatedBy
                              ,DateCreated
                              ,UpdatedBy
                              ,DateUpdated
                          FROM tblC2PTransactionType
                          WHERE TransactionTypeID = @Id";

            var result = await _db.QueryFirstOrDefaultAsync<TransactionTypeEntity>(query, param);
            return result;
        }

        public async Task<TransactionTypeEntity> GetTransactionTypeByDescription(string desc)
        {
            var param = new { Desc = desc };
            var query = @"SELECT TransactionTypeID
                              ,TransactionTypeDescription
                              ,CreatedBy
                              ,DateCreated
                              ,UpdatedBy
                              ,DateUpdated
                          FROM tblC2PTransactionType
                          WHERE TransactionTypeDescription = @Desc";

            var result = await _db.QueryFirstOrDefaultAsync<TransactionTypeEntity>(query, param);
            return result;
        }
    }
}
