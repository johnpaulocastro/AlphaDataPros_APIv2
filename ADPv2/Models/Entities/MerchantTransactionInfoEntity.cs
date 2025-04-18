using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ADPv2.Models.Entities
{
    public class MerchantTransactionInfoEntity : BaseEntity
    {
        public int ID { get; set; }
        public string MerchantID { get; set; }
        public decimal TotalSalesPerMonth { get; set; }
        public int NumberTransactionPerMonth { get; set; }
        public decimal MinimumTicketAmount { get; set; }
        public decimal MaximumTicketAmount { get; set; }
    }
}
