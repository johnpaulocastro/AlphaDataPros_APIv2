using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IPaymentLinkRepository
    {
        Task<PaymentLinkEntity> GetPaymentLink(string paymentLinkId);
        Task<List<PaymentLinkEntity>> GetPaymentLinkList(string merchantId);
        Task<int> CreatePaymentLink(PaymentLinkEntity entity);
        Task<bool> UpdatePaymentLink(PaymentLinkEntity entity);
    }
}
