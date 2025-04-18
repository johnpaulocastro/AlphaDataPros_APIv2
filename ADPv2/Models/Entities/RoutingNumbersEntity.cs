namespace ADPv2.Models.Entities
{
    public class RoutingNumbersEntity : BaseEntity
    {
        public int ID { get; set; }
        public string RoutingNumber { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankCity { get; set; }
        public string BankState { get; set; }
        public string BankZipCode { get; set; }
        public string BankZipCode2 { get; set; }
        public string PhoneNumber { get; set; }
    }
}
