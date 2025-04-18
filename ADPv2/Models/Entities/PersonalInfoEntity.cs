namespace ADPv2.Models.Entities
{
    public class PersonalInfoEntity : BaseEntity
    {
        public int ID { get; set; }
        public string PersonalInfoId { get; set; }
        public string CustomerNo { get; set; }
        public string SaltData { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string SocialSecurity { get; set; }
        public string Profession { get; set; }
        public string EmailAddress { get; set; }
        public string AlternateEmail { get; set; }
        public string MobileNumber { get; set; }
        public string LandlineNumber { get; set; }

        public int Status { get; set; }
        public string StatusDescription { get; set; }

        public string DefaultArkcode { get; set; }

        public string CustomerName
        {
            get
            {
                return string.Format("{0} {1} {2}", this.FirstName, this.MiddleName, this.LastName);
            }
        }
    }
}
