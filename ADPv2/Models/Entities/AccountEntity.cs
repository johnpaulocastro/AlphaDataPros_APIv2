using System.ComponentModel.DataAnnotations;

namespace ADPv2.Models.Entities
{
    public class AccountEntity : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        public string PersonalInfoId { get; set; } = string.Empty;
        public string CustomerNo { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string SecureKey { get; set; } = string.Empty;
        public string SecureIV { get; set; } = string.Empty;
        public int Role { get; set; }
        public int UserStatus { get; set; }
        public int AccountStatus { get; set; }
        public bool IsTemporary { get; set; }
        public bool HasTwoFactor { get; set; }
        public DateTime PasswordExpiry { get; set; }
        public DateTime LastLoggedIn { get; set; }
    }
}
