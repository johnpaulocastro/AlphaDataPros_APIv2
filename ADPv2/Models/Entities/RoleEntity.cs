namespace ADPv2.Models.Entities
{
    public class RoleEntity : BaseEntity
    {
        public int ID { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsOption { get; set; }
        public bool Status { get; set; }
    }
}
