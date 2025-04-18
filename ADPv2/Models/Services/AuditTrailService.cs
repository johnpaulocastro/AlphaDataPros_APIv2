using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;

namespace ADPv2.Models.Services
{
    public class AuditTrailService : IAuditTrailService
    {
        private readonly IAuditTrailRepository _repository;
        
        public AuditTrailService(IAuditTrailRepository repository)
        {
            this._repository = repository;
        }

        public async Task CreateAuditTrail(AuditTrailEntity entity)
        {
            await this._repository.CreateAuditTrail(entity);
        }
    }
}
