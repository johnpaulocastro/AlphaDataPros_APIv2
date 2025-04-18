using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<RoleEntity>> GetRoleList();
    }
}
