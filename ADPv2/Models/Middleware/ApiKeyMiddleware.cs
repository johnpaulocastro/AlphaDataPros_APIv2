using ADPv2.Models.ViewModels;
using Microsoft.Extensions.Primitives;

namespace ADPv2.Models.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ApiKeyMiddleware(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

        public async Task InvokeAsync(HttpContext context)
        {
            const string _authToken = "Authorization";
            context.Request.Headers.TryGetValue(_authToken, out StringValues headerValue);
            if (context.Items.ContainsKey(_authToken))
            {
                context.Items[_authToken] = headerValue[0].ToString().Replace("Bearer ", string.Empty);
            }
            else
            {
                if (headerValue.Count > 0)
                {
                    var _headerValue = headerValue[0].ToString().Replace("Bearer ", string.Empty);
                    context.Items.Add(_authToken, $"{_headerValue}");
                }
            }

            await _requestDelegate(context);
        }
    }
}
