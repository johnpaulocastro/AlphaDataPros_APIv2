using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IRoutingNumbersService
    {
        Task<RoutingNumbersEntity> GetRoutingNumberNumberById(int id);
        Task<RoutingNumbersEntity> GetRoutingNumberByRoutingNumber(int routingNumber);
        Task<int> CreateRoutingNumber(RoutingNumbersEntity entity);
        Task<bool> UpdateRoutingNumber(RoutingNumbersEntity entity);
    }
}
