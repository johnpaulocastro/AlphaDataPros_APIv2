namespace ADPv2.Models.Entities
{
    public class TransactionHistoryDetailsEntity
    {
        public int TransactionDetailsNo { get; set; }
        public int TransactionIdNo { get; set; }
        public int TransactionApprovalStatusID { get; set; }
        public int TransactionStatusID { get; set; }
        public int TransactionTypeID { get; set; }
        public int ReturnCodeID { get; set; }
        public string TransactionNotes { get; set; } = string.Empty;
        public int IsDeleted { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? DateCreated { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? DateUpdated { get; set; }
    }
}
