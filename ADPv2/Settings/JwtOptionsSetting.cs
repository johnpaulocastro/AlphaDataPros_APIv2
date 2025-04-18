namespace ADPv2.Settings
{
    public record class JwtOptionsSetting (
            string Issuer,
            string Audience,
            string SigningKey,
            int ExpirationSeconds
        );
}
