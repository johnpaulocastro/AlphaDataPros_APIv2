using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Microsoft.Extensions.Options;

namespace ADPv2.Models.Services
{
    public class PaymentLinkService : IPaymentLinkService
    {
        private PaymentLinkSettings _paymentLinkSettings;
        private IPaymentLinkRepository _paymentLinkRepository;

        public PaymentLinkService(IOptions<PaymentLinkSettings> paymentLinkOptions, 
            IPaymentLinkRepository paymentLinkRepository)
        {
            this._paymentLinkSettings = paymentLinkOptions.Value;
            this._paymentLinkRepository = paymentLinkRepository;

        }

        public async Task<PaymentLinkEntity> GetPaymentLink(string paymentLinkId)
        {
            var response = await _paymentLinkRepository.GetPaymentLink(paymentLinkId);
            if (response != null && response.ExpiryDate >= DateTime.UtcNow)
            {
                return response;
            }

            return null;
        }

        public async Task<List<PaymentLinkEntity>> GetPaymentLinkList(string merchantId)
        {
            return await _paymentLinkRepository.GetPaymentLinkList(merchantId);
        }

        public async Task<string> CreatePaymentLink(string merchantId)
        {
            var paymentLink = new PaymentLinkEntity();
            paymentLink.MerchantId = merchantId;
            paymentLink.PaymentLinkId = Guid.NewGuid().ToString();
            paymentLink.PaymentLinkUrl = $"{_paymentLinkSettings.BaseUrl}/Payment/PayByLink?ID={paymentLink.PaymentLinkId}";
            paymentLink.CreatedDate = DateTime.UtcNow;
            paymentLink.ExpiryDate = paymentLink.CreatedDate.AddMinutes(10);
            
            var response = await _paymentLinkRepository.CreatePaymentLink(paymentLink);
            return response > 0 ? paymentLink.PaymentLinkUrl : null;
        }

        public async Task<string> CreateCreditCardPaymentLink(string merchantId, string amount)
        {
            var paymentLink = new PaymentLinkEntity();
            paymentLink.MerchantId = merchantId;
            paymentLink.PaymentLinkId = Guid.NewGuid().ToString();
            paymentLink.PaymentLinkUrl = $"{_paymentLinkSettings.BaseUrl}/Payment/PayByLink?ID={paymentLink.PaymentLinkId}";
            paymentLink.IsCreditCard = true;
            paymentLink.Amount = amount;
            paymentLink.CreatedDate = DateTime.UtcNow;
            paymentLink.ExpiryDate = paymentLink.CreatedDate.AddMinutes(10);
            
            var response = await _paymentLinkRepository.CreatePaymentLink(paymentLink);
            return response > 0 ? paymentLink.PaymentLinkUrl : null;
        }

        public async Task<bool> UpdatePaymentLink(PaymentLinkEntity paymentLinkEntity)
        {
            return await _paymentLinkRepository.UpdatePaymentLink(paymentLinkEntity);
        }
    }
}
