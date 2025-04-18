using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ADPv2.Models.Entities
{
    public class MerchantOtherInfoEntity : BaseEntity
    {
        public int ID { get; set; }
        public string MerchantID { get; set; }
        public string OtherInformationCode { get; set; }
        public string AgentName { get; set; }
    }
}
