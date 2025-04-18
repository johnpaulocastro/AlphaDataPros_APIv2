using ADPv2.Models.ViewModels;
using ADPv2.Models.ViewModels.ApiViewModels;
using ADPv2.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ADPv2.Models.Middleware
{
    public interface ITokenEndpoint
    {
        Task<TokenResponseDto> Connect(ApiAccountCredentialsRequestDto requestDto);
        string CreateAccessToken(ApiAccountCredentialsRequestDto requestDto, TimeSpan expiration, string[] permissions);
        Task<ApiKeyResponseDto> Decode(HttpContext context);
    }

    public class TokenEndpointMiddleware : ITokenEndpoint
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        public TokenEndpointMiddleware(IOptions<JwtSettings> settings)
        {
            this._jwtSettings = settings;
        }

        public string CreateAccessToken(
            ApiAccountCredentialsRequestDto request,
            TimeSpan expiration,
            string[] permissions)
        {
            var guidId = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var keyBytes = Encoding.UTF8.GetBytes(_jwtSettings.Value.SigningKey);
            var symmetricKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("sub", request.Username),
                new Claim("name", request.Username),
                new Claim("aud", _jwtSettings.Value.Audience)
            };

            var roleClaims = permissions.Select(x => new Claim("role", x));
            claims.AddRange(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Audience,
                claims: claims,
                expires: DateTime.Now.Add(expiration),
                signingCredentials: signingCredentials);

            var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
            return rawToken;
        }

        public async Task<TokenResponseDto> Connect(ApiAccountCredentialsRequestDto requestDto)
        {
            //creates the access token (jwt token)
            var tokenExpiration = TimeSpan.FromSeconds(_jwtSettings.Value.ExpirationSeconds);
            var accessToken = CreateAccessToken(
                requestDto,
                TimeSpan.FromMinutes(60),
                new[] { "read_todo", "create_todo" });

            //returns a json response with the access token
            return new TokenResponseDto
            {
                access_token = accessToken,
                expiration = (int)tokenExpiration.TotalSeconds,
                type = "bearer"
            };
        }

        public async Task<ApiKeyResponseDto> Decode(HttpContext context)
        {
            context.Items.TryGetValue("Authorization", out object? value);

            var jwt = value.ToString();
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            var name = token.Claims.First(claim => claim.Type == "name").Value;

            return new ApiKeyResponseDto
            {
                Authorization = value.ToString(),
                Name = name
            };
        }
    }
}
