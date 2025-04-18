using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;

namespace ADPv2.Models.Services
{
    public class RoutingNumbersService : IRoutingNumbersService
    {
        private readonly IRoutingNumbersRepository _routingNumbersRepository;

        public RoutingNumbersService(IRoutingNumbersRepository routingNumbersRepository)
        {
            _routingNumbersRepository = routingNumbersRepository;
        }

        public async Task<int> CreateRoutingNumber(RoutingNumbersEntity entity)
        {
            return await _routingNumbersRepository.CreateRoutingNumber(entity);
        }

        public async Task<RoutingNumbersEntity> GetRoutingNumberByRoutingNumber(int routingNumber)
        {
            return await _routingNumbersRepository.GetRoutingNumberByRoutingNumber(routingNumber);
        }

        public async Task<RoutingNumbersEntity> GetRoutingNumberNumberById(int id)
        {
            return await _routingNumbersRepository.GetRoutingNumberNumberById(id);
        }

        public async Task<bool> UpdateRoutingNumber(RoutingNumbersEntity entity)
        {
            return await _routingNumbersRepository.UpdateRoutingNumber(entity);
        }
    }
}
