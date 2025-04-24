using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using Square.Models;

namespace ADPv2.Models.Repositories
{
    public class PaymentLinkRepository : BaseRepository, IPaymentLinkRepository
    {
        public PaymentLinkRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<PaymentLinkEntity> GetPaymentLink(string paymentLinkId)
        {
            string query = string.Format(@"SELECT * FROM tblPaymentLink WHERE PaymentLinkId = @PaymentLinkId AND IsDeleted = 0");
            var parameters = new { PaymentLinkId = paymentLinkId };

            var result = await _db.QueryFirstOrDefaultAsync<PaymentLinkEntity>(query, parameters);
            return result;
        }

        public async Task<List<PaymentLinkEntity>> GetPaymentLinkList(string merchantId)
        {
            string query = string.Format(@"SELECT * FROM tblPaymentLink WHERE MerchantId = @MerchantId AND IsDeleted = 0");
            var parameters = new { MerchantId = merchantId };

            var result = await _db.QueryAsync<PaymentLinkEntity>(query, parameters);
            return result.ToList();
        }

        public async Task<int> CreatePaymentLink(PaymentLinkEntity entity)
        {
            return await _db.InsertAsync<PaymentLinkEntity>(entity);
        }

        public async Task<bool> UpdatePaymentLink(PaymentLinkEntity entity)
        {
            return await _db.UpdateAsync<PaymentLinkEntity>(entity);
        }
    }
}
