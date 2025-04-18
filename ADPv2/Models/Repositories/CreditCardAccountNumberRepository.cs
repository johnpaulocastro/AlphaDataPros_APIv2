using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;

namespace ADPv2.Models.Repositories
{
    public class CreditCardAccountNumberRepository : BaseRepository, ICreditCardAccountNumberRepository
    {
        public CreditCardAccountNumberRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<CreditCardAccountNumberEntity> GetCreditCardAccountNumberById(int id)
        {
            return await _db.QueryFirstOrDefaultAsync<CreditCardAccountNumberEntity>("SELECT * FROM tblCreditCardAccountNumber WHERE ID = @ID", new { ID = id });
        }

        public async Task<List<CreditCardAccountNumberEntity>> GetCreditCardAccountNumberByMerchantId(string merchantId)
        {
            var response = await _db.QueryAsync<CreditCardAccountNumberEntity>("SELECT * FROM tblCreditCardAccountNumber WHERE MerchantId = @MerchantId", new { MerchantId = merchantId });
            return response.ToList();
        }

        public async Task<CreditCardAccountNumberEntity> GetCreditCardAccountNumberByAccountNumber(int accountNumber)
        {
            return await _db.QueryFirstOrDefaultAsync<CreditCardAccountNumberEntity>("SELECT * FROM tblCreditCardAccountNumber WHERE AccountNumber = @AccountNumber", new { AccountNumber = accountNumber });
        }

        public async Task<CreditCardAccountNumberEntity> GetCreditCardAccountNumberByRoutingNumber(int routingNumber)
        {
            return await _db.QueryFirstOrDefaultAsync<CreditCardAccountNumberEntity>("SELECT * FROM tblCreditCardAccountNumber WHERE RoutingNumber = @RoutingNumber", new { RoutingNumber = routingNumber });
        }

        public async Task<int> CreateCreditCardAccountNumber(CreditCardAccountNumberEntity entity)
        {
            return await _db.InsertAsync<CreditCardAccountNumberEntity>(entity);
        }

        public async Task<bool> UpdateCreditCardAccountNumber (CreditCardAccountNumberEntity entity)
        {
            return await _db.UpdateAsync<CreditCardAccountNumberEntity>(entity);
        }
    }
}
