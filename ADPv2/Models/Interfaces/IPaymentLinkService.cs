using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface IPaymentLinkService
    {
        Task<PaymentLinkEntity> GetPaymentLink(string paymentLinkId);
        Task<List<PaymentLinkEntity>> GetPaymentLinkList(string merchantId);
        Task<string> CreatePaymentLink(string merchantId);
        Task<string> CreateCreditCardPaymentLink(string merchantId, string amount);
        Task<bool> UpdatePaymentLink(PaymentLinkEntity paymentLinkEntity);
    }
}
