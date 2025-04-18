using ADPv2.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ADPv2.Models.ViewModels
{
    public class AccountMerchantRegistrationRequestDto
    {
        public string ResellerID { get; set; }
        public AccountMerchantInfoRequestDto MerchantInfo { get; set; }
        public AccountMerchantCompanyInfoRequestDto MerchantCompanyInfo { get; set; }
        public AccountMerchantProcessingHistoryInfoRequestDto MerchantProcessingInfo { get; set; }
        public AccountMerchantTransactionInfoRequestDto MerchantTransactionInfo { get; set; }
        public AccountMerchantOtherInfoRequestDto MerchantOtherInfo { get; set; }
    }

    public class AccountMerchantInfoRequestDto
    {
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
    }

    public class AccountMerchantCompanyInfoRequestDto
    {
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

    public class AccountMerchantProcessingHistoryInfoRequestDto
    {
        public bool IsAcceptingCreditCard { get; set; }
        public bool HasBeenProcessedBefore { get; set; }
        public bool IsAccountTerminated { get; set; }
        public string Reason { get; set; }
        public string FormerProcessor { get; set; }
        public string TimeProcessor { get; set; }
    }

    public class AccountMerchantTransactionInfoRequestDto
    {
        public decimal TotalSalesPerMonth { get; set; }
        public int NumberTransactionPerMonth { get; set; }
        public decimal MinimumTicketAmount { get; set; }
        public decimal MaximumTicketAmount { get; set; }
    }

    public class AccountMerchantOtherInfoRequestDto
    {
        public string OtherInformationCode { get; set; }
        public string AgentName { get; set; }
    }

}
