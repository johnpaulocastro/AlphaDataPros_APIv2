namespace ADPv2.Models.Entities
{
    public class TransactionHistoryEntity : BaseEntity
    {
        public int ID { get; set; }

        private string _transactionNo { get; set; }
        public string TransactionNo
        {
            set
            {
                _transactionNo = value;
            }
            get
            {
                return _transactionNo.PadLeft(6, '0');
            }
        }
        public int TransactionApprovalStatusID { get; set; }
        public int TransactionStatusID { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerStreetNumber { get; set; }
        public string CustomerStreetUnitNumber { get; set; }
        public string CustomerStreetName { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerZipCode { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string BankName { get; set; }
        public string BankRoutingNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string CheckNo { get; set; }
        public decimal TransactionRate { get; set; }
        public decimal CreditCardTransactionRate { get; set; }
        public decimal TransactionCommissionRate { get; set; }
        public decimal TransactionCompanyCommissionRate { get; set; }
        public decimal TransactionFee { get; set; }
        public decimal CreditCardTransactionFee { get; set; }
        public decimal TransactionCommissionFee { get; set; }
        public decimal TransactionCompanyCommissionFee { get; set; }
        public decimal RollingReserve { get; set; }
        public decimal RollingReservePeakSales { get; set; }
        public decimal GiactCharge { get; set; }
        public decimal ArkcodeDiscount { get; set; }
        public string TransactionNotes { get; set; }
        public bool IsPrintQue { get; set; }
        public bool IsPrinted { get; set; }
        public string MerchantId { get; set; }
        public string PersonalInfoId { get; set; }
        public int? CheckingAccountID { get; set; }
        public string TransactedBank { get; set; }
        public DateTime? DatePrintQue { get; set; }
    }
}
