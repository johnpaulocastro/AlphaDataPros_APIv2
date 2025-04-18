namespace ADPv2.Models.Entities
{
    public class EWalletEntity : BaseEntity
    {
        public int ID { get; set; }
        public string PersonalInfoId { get; set; }
        public string CustomerNo { get; set; }
        public bool IsActivated { get; set; }
        public decimal Balance { get; set; }
    }
}
