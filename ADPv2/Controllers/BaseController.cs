using ADPv2.Models.Interfaces;
using ADPv2.Models.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ADPv2.Controllers
{
    public class BaseController : Controller
    {
        //protected IHttpContextAccessor _httpContext;
        protected IAccountService _accountService;
        protected IAuditTrailService _auditTrailService;
        protected ITokenEndpoint _tokenEndpoint;
        protected ILogger<AccountController> _logger;
        protected IHttpContextAccessor _httpContextAccessor;

        protected string Username;

        public BaseController() { }

        public BaseController(
            IHttpContextAccessor httpContext,
            ITokenEndpoint tokenEndpoint
            )
        {
            var tokenResponse = tokenEndpoint.Decode(httpContext.HttpContext);
            this.Username = tokenResponse.Result.Name;
        }
    }
}
