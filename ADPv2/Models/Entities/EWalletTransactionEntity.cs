namespace ADPv2.Models.Entities
{
    public class EWalletTransactionEntity : BaseEntity
    {
        public int ID { get; set; }
        public string PersonalInfoId { get; set; }
        public string MerchantId { get; set; }
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionDescription { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
    }
}
