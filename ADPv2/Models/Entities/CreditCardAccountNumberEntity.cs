namespace ADPv2.Models.Entities
{
    public class CreditCardAccountNumberEntity : BaseEntity
    {
        public int ID { get; set; }
        public string MerchantID { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }
        public string BankName { get; set; }
        public Boolean IsRREnabled { get; set; }
    }
}
