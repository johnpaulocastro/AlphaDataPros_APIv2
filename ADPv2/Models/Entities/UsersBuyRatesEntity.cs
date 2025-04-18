namespace ADPv2.Models.Entities
{
    public class UsersBuyRatesEntity : BaseEntity
    {
        public int ID { get; set; }
        public string MerchantId { get; set; }
        public decimal TransactionRate { get; set; }
        public decimal CreditCardTransactionRate { get; set; }
        public decimal TransactionCommisionRate { get; set; }
        public decimal TransactionCompanyCommisionRate { get; set; }
        public decimal TransactionFee { get; set; }
        public decimal CreditCardTransactionFee { get; set; }
        public decimal TransactionCommisionFee { get; set; }
        public decimal TransactionCompanyCommisionFee { get; set; }
        public decimal RollingReserve { get; set; }
        public decimal RollingReservePeakSales { get; set; }
        public decimal RollingReservePeakSalesAmount { get; set; }
        public bool IsPhyCheckHasRR { get; set; }
        public decimal BankReturnFee { get; set; }
        public decimal ReturnFee { get; set; }
        public decimal RefundFee { get; set; }
        public decimal ArkcodeDiscount { get; set; }
        public decimal PreNsf { get; set; }
        public decimal PostNsf { get; set; }
        public decimal PerCallFee { get; set; }
        public decimal GiactCharge { get; set; }
        public decimal WireFee { get; set; }
    }
}
