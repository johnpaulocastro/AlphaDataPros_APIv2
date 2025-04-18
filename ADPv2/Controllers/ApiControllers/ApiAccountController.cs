using ADPv2.Models.Interfaces;
using ADPv2.Models.Middleware;
using ADPv2.Models.ViewModels.ApiViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using Serilog.Context;
using Serilog.Core;

namespace ADPv2.Controllers.ApiControllers
{
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/account")]
    public class ApiAccountController : BaseController
    {
        public ApiAccountController(
            IAccountService accountService,
            IAuditTrailService auditTrailService,
            ITokenEndpoint tokenEndpoint,
            ILogger<AccountController> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            this._accountService = accountService;
            this._auditTrailService = auditTrailService;
            this._tokenEndpoint = tokenEndpoint;
            this._logger = logger;

            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Authenticate the credentials provided by the user
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Authenticate([FromBody] ApiAccountCredentialsRequestDto request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            // check if username exists
            var account = await _accountService.GetAccount(request.Username);
            if (account == null)
            {
                _logger.LogError($"{request.Username} : User not found.");
                return NotFound(Results.NotFound(new { message = "User not found." }));
            }

            // check if password is valid
            var isValid = await _accountService.Authenticate(request);
            if (!isValid)
            {
                _logger.LogError($"{request.Username} : User attempted to login with incorrect Password");
                return BadRequest("Invalid credentials");
            }

            if (account.SecureKey != request.SecureKey)
            {
                _logger.LogError($"{request.Username} : User attempted to login with incorrect SecureKey");
                return BadRequest("Invalid credentials");
            }

            // create a JWT Token based on account
            var response = await _tokenEndpoint.Connect(request);
            _logger.LogInformation($"{request.Username} : user successfully authenticated. Token has been generated.");

            return Ok(response);
        }
    }
}
