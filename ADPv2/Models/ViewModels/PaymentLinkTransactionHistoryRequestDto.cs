namespace ADPv2.Models.ViewModels
{
    public class PaymentLinkTransactionHistoryRequestDto
    {
        public string PaymentLinkId { get; set; }
        public TransactionHistoryRequestDto Transactions { get;set; }
    }

    public class PaymentLinkCCardTransactionHistoryRequestDto
    {
        public string PaymentLinkId { get; set; }
        public TransactionHistoryCCardRequestDto Transactions { get;set; }
    }
}
