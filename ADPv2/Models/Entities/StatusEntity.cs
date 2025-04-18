namespace ADPv2.Models.Entities
{
    public class StatusEntity : BaseEntity
    {
        public int ID { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal TransactionFeeAmount { get; set; }
        public bool Mode { get; set; }
        public bool IsApprovalStatus { get; set; }
        public bool IsTransactionStatus { get; set; }
        public bool ForStatus { get; set; }
        public bool ForStatusType { get; set; }
    }
}
