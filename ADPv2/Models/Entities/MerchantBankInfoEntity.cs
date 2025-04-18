namespace ADPv2.Models.Entities
{
    public class MerchantBankInfoEntity : BaseEntity
    {
        public int ID { get; set; }
        public string MerchantID { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountHolderAddress { get; set; }
        public string AccountHolderCountry { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankCountry { get; set; }
        public string BankSwiftCode { get; set; }
        public string BankRoutingNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
