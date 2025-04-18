using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace ADPv2.Models.Repositories
{
    public class EWalletRepository : BaseRepository, IEWalletRepository
    {
        public EWalletRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<EWalletEntity> GetEwallet(string customerId)
        {
            var query = "SELECT * FROM tblEWallet WHERE PersonalInfoId = @CustomerId";
            var param = new { CustomerId = customerId };

            return await _db.QueryFirstOrDefaultAsync<EWalletEntity>(query, param);
        }

        public async Task<bool> UpdateEWallet(EWalletEntity entity)
        {
            var query = @"UPDATE tblEWallet SET 
                                Balance = @Balance, 
                                UpdatedBy = @UpdatedBy,
                                DateUpdated = @DateUpdated 
                            WHERE PersonalInfoId = @PersonalInfoId";
            var param = new
            {
                Balance = entity.Balance,
                UpdatedBy = entity.UpdatedBy,
                DateUpdated = entity.DateUpdated,
                PersonalInfoId = entity.PersonalInfoId
            };

            var result = await _db.ExecuteAsync(query, param);
            return result > 0;
        }
    }
}
