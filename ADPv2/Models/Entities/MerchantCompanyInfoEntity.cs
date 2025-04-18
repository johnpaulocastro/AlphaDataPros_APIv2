using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ADPv2.Models.Entities
{
    public class MerchantCompanyInfoEntity : BaseEntity
    {
        public int ID { get; set; }
        public string MerchantID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyStreetNo { get; set; }
        public string CompanyStreetName { get; set; }
        public string CompanyUnitNo { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyZipCode { get; set; }
        public string CompanyCountry { get; set; }
        public string CompanyWebsiteURL { get; set; }
        public string CompanyProduct { get; set; }
        public string CompanyDescriptor { get; set; }
        public string CSRContactName { get; set; }
        public string CSRContactPhoneNo { get; set; }
        public string CSREmailAddress { get; set; }
    }
}
