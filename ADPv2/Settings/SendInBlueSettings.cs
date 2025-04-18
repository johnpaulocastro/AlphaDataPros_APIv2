namespace ADPv2.Settings
{
    public class SendInBlueSettings
    {
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }

        public int Port { get; set; }
        public string Host { get; set; }
        public string SigningKey { get; set; }
        public string SmsSigningKey { get; set; }
    }

    public class SendInBlueCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
