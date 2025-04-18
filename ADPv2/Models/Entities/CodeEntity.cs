namespace ADPv2.Models.Entities
{
    public class CodeEntity
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public string AlphaCode { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
