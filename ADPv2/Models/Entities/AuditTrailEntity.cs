namespace ADPv2.Models.Entities
{
    public class AuditTrailEntity
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string IPAddress { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
