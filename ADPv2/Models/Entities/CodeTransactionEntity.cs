namespace ADPv2.Models.Entities
{
    public class CodeTransactionEntity : BaseEntity
    {
        public int ID { get; set; }
        public string TransactionId { get; set; }
        public string PersonalInfoId { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime PostingDate { get; set; }
        public string TransactionDescription { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }

        public decimal TransactionRate { get; set; }
        public decimal TransactionCommisionRate { get; set; }
        public decimal TransactionCompanyCommisionRate { get; set; }
        public decimal TransactionFee { get; set; }
        public decimal TransactionCommisionFee { get; set; }
        public decimal TransactionCompanyCommisionFee { get; set; }
        public decimal RollingReserve { get; set; }
        public decimal RollingReservePeakSales { get; set; }
        public decimal ReturnFee { get; set; }
        public decimal RefundFee { get; set; }
        public decimal ArkcodeDiscount { get; set; }
    }
}
