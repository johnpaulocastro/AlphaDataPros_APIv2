using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace ADPv2.Models.Repositories
{
    public class StatusRepository : BaseRepository, IStatusRepository
    {
        public StatusRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<StatusEntity> GetStatus(int statusId)
        {
            var query = "SELECT * FROM tblStatus WHERE ID = @ID";
            var param = new
            {
                ID = statusId,
            };

            var result = await _db.QueryFirstOrDefaultAsync<StatusEntity>(query, param);
            return result;
        }
    }
}
