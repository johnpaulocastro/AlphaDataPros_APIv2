namespace ADPv2.Models.ViewModels
{
    public class EWalletTransactionRequestDto
    {
        public string TransactionId { get; set; }
        public string CustomerId { get; set; }
        public string MerchantId { get; set; }
        public string TransactionDescription { get; set; }
        public decimal Amount { get; set; }
    }
}
