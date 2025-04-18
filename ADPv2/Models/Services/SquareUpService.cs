using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Microsoft.Extensions.Options;

namespace ADPv2.Models.Services
{
    public class SquareUpService : ISquareUpService
    {
        private readonly StripeSetting _setting;

        public SquareUpService(IOptions<StripeSetting> setting)
        {
            _setting = setting.Value;
        }

        public async Task<string> GetToken()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_setting.BaseUrl}{_setting.TokenLocation}");
            request.Headers.Add("Square-Version", _setting.SquareVersion);
            request.Headers.Add("Authorization", $"Bearer {_setting.AccessToken}");

            var content = new StringContent(string.Empty);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }
    }
}
