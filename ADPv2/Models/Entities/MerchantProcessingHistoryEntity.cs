using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ADPv2.Models.Entities
{
    public class MerchantProcessingHistoryEntity : BaseEntity
    {
        public int ID { get; set; }
        public string MerchantID { get; set; }
        public bool IsAcceptingCreditCard { get; set; }
        public bool HasBeenProcessedBefore { get; set; }
        public bool IsAccountTerminated { get; set; }
        public string Reason { get; set; }
        public string FormerProcessor { get; set; }
        public string TimeProcessor { get; set; }
    }
}
