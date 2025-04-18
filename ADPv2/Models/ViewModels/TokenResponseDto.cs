namespace ADPv2.Models.ViewModels
{
    public class TokenResponseDto
    {
        public string access_token { get; set; }
        public int expiration { get; set; }
        public string type { get; set; }
    }
}
