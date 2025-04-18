using ADPv2.Models.Entities;
using ADPv2.Settings;
using Dapper.Contrib.Extensions;
using Dapper;
using Microsoft.Extensions.Options;
using ADPv2.Models.Interfaces;

namespace ADPv2.Models.Repositories
{
    public class RoutingNumbersRepository : BaseRepository, IRoutingNumbersRepository
    {
        public RoutingNumbersRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<RoutingNumbersEntity> GetRoutingNumberNumberById(int id)
        {
            return await _db.QueryFirstOrDefaultAsync<RoutingNumbersEntity>("SELECT * FROM tblC2PRoutingNumbers WHERE ID = @ID", new { ID = id });
        }

        public async Task<RoutingNumbersEntity> GetRoutingNumberByRoutingNumber(int routingNumber)
        {
            return await _db.QueryFirstOrDefaultAsync<RoutingNumbersEntity>("SELECT * FROM tblC2PRoutingNumbers WHERE RoutingNumber = @RoutingNumber", new { RoutingNumber = routingNumber });
        }

        public async Task<int> CreateRoutingNumber(RoutingNumbersEntity entity)
        {
            return await _db.InsertAsync<RoutingNumbersEntity>(entity);
        }

        public async Task<bool> UpdateRoutingNumber(RoutingNumbersEntity entity)
        {
            return await _db.UpdateAsync<RoutingNumbersEntity>(entity);
        }
    }
}
