using System.Text.Json.Serialization;

namespace ADPv2.Models.Entities
{
    public class BaseEntity
    {
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
