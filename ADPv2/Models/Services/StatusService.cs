using ADPv2.Models.Interfaces;
using ADPv2.Models.ViewModels;

namespace ADPv2.Models.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository statusRepository;
        public StatusService(IStatusRepository statusRepository)
        {
            this.statusRepository = statusRepository;
        }

        public async Task<string> GetDescription(int statusId)
        {
            var result = await statusRepository.GetStatus(statusId);
            return result.Description;
        }
    }
}
