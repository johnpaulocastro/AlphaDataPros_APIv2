namespace ADPv2.Models.ViewModels
{
    public class TransactionHistoryCreateRequestDto
    {
        public int TransactionId { get; set; }
        public int ApprovalStatusId { get; set; }
        public int TransactionStatusId { get; set; }
        public string TransactionNotes { get; set; }
        public string UserLogged { get; set; }
    }
}
