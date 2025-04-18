using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace ADPv2.Models.Repositories
{
    public class UserBuyRateRepository : BaseRepository, IUserBuyRateRepository
    {
        public UserBuyRateRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<UsersBuyRatesEntity> GetUserBuyRate(string merchantId)
        {
            var query = @"SELECT ID
                                ,MerchantId
                                ,CAST(TransactionRate * 100 as decimal(10,2)) TransactionRate
                                ,CAST(TransactionCommisionRate * 100 as decimal(10,2)) TransactionCommisionRate
                                ,CAST(TransactionCompanyCommisionRate * 100 as decimal(10,2)) TransactionCompanyCommisionRate
                                ,TransactionFee
                                ,TransactionCommisionFee
                                ,TransactionCompanyCommisionFee
                                ,CAST(RollingReserve * 100 as decimal(10,2)) RollingReserve
                                ,ReturnFee
                                ,RefundFee
                                ,CAST(ArkcodeDiscount * 100 as decimal(10,2)) ArkcodeDiscount
                                ,PreNsf
                                ,PostNsf
                                ,PerCallFee
                                ,GiactCharge
                                ,WireFee
                                ,CreatedBy
                                ,DateCreated
                                ,UpdatedBy
                                ,DateUpdated
                            FROM tblUsersBuyRates
                            WHERE MerchantId = @MerchantId";

            var result = await _db.QueryFirstOrDefaultAsync<UsersBuyRatesEntity>(query, new { MerchantId = merchantId });
            return result;
        }
    }
}
