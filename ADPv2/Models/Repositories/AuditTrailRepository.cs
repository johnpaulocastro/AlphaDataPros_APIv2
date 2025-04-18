using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using System.Net;

namespace ADPv2.Models.Repositories
{
    public class AuditTrailRepository : BaseRepository, IAuditTrailRepository
    {
        public AuditTrailRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task CreateAuditTrail(AuditTrailEntity entity)
        {
            var query = "INSERT INTO tblAuditTrail (Username, Action, IPAddress, Timestamp) VALUES (@Username, @Action, @IPAddress, @Timestamp)";
            var param = new
            {
                Username = entity.Username,
                Action = entity.Action,
                IPAddress = entity.IPAddress,
                Timestamp = entity.Timestamp
            };

            await _db.ExecuteAsync(query, param);
        }
    }
}
