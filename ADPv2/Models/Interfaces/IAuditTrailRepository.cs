using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IAuditTrailRepository
    {
        Task CreateAuditTrail(AuditTrailEntity entity);
    }
}
