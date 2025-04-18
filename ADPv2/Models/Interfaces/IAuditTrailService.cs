using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IAuditTrailService
    {
        Task CreateAuditTrail(AuditTrailEntity entity);
    }
}
