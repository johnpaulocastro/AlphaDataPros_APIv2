using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace ADPv2.Models.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<List<RoleEntity>> GetRoleList()
        {
            return (List<RoleEntity>)await _db.QueryAsync<RoleEntity>("SELECT ID, Description, IsOption, Status, CreatedBy, UpdatedBy, DateCreated, DateUpdated");
        }
    }
}
