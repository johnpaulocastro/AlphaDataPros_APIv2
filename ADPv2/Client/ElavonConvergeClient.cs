using ADPv2.Settings;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;

namespace ADPv2.Client
{
    public interface IElavonConvergeClient
    {
        Task<object> GetSessionToken();
    }

    public class ElavonConvergeClient: IElavonConvergeClient
    {
        private readonly ElavonConvergeSettings _settings;
        private readonly IFlurlClient _flurlClient;
        private readonly Flurl.Url _baseUrl;

        public ElavonConvergeClient(
            IOptions<ElavonConvergeSettings> settings
        )
        {
            this._settings = settings.Value;
            this._baseUrl = new Flurl.Url(_settings.BaseUrl);
        }

        public async Task<object> GetSessionToken()
        {
            try
            {
                var response = await _baseUrl
                    .AppendPathSegment("/hosted-payments/transaction_token")
                    .PostUrlEncodedAsync(new
                    {
                        ssl_transaction_type = "ccsale",
                        ssl_account_id = _settings.SSLAccountId,
                        ssl_user_id = _settings.SSLUserId,
                        ssl_pin = _settings.SSLPIN
                    })
                    .ReceiveString();
                    
                return response;
            }
            catch( Exception ex ) {
                throw;
            }
        }
    }
}
