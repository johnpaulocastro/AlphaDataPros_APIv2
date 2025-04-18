using System.ComponentModel.DataAnnotations.Schema;

namespace ADPv2.Models.Entities
{
    [Table(name: "tblPaymentLink")]
    public class PaymentLinkEntity
    {
        public int ID { get; set; }
        public string MerchantId { get; set; }
        public string PaymentLinkId { get; set; }
        public string PaymentLinkUrl { get; set; }
        public bool IsCreditCard { get; set; } = false;
        public string Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
