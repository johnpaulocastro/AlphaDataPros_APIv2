using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ADPv2.Models.Entities
{
    public class MerchantInfoEntity : BaseEntity
    {
        public int ID { get; set; }
        public string ResellerID { get; set; }
        public string MerchantID { get; set; }
        public string MerchantName { get; set; }
        public string MerchantStreetNo { get; set; }
        public string MerchantStreetName { get; set; }
        public string MerchantUnitNo { get; set; }
        public string MerchantCity { get; set; }
        public string MerchantState { get; set; }
        public string MerchantZipCode { get; set; }
        public string MerchantCountry { get; set; }
        public string MerchantPhoneNo { get; set; }
        public string MerchantMobileNo { get; set; }
        public string MerchantEmailAddress { get; set; }
        public string MerchantSSN { get; set; }
        public string MerchantPassportNo { get; set; }
        public bool ArkcodeEnable { get; set; }
        public bool DisableCreateTransaction { get; set; }
    }
}
