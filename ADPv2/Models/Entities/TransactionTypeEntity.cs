namespace ADPv2.Models.Entities
{
    public class TransactionTypeEntity : BaseEntity
    {
        public int TransactionTypeID { get; set; }
        public string TransactionTypeDescription { get; set; } = string.Empty;
    }
}
