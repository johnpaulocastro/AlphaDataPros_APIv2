using ADPv2.Models.Interfaces;
using ADPv2.Models.Middleware;
using ADPv2.Models.ViewModels;
using ADPv2.Models.ViewModels.ApiViewModels;
using Azure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Policy;

namespace ADPv2.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("account")]
    public class AccountController : BaseController
    {
        private readonly IMerchantService _merchantService;

        public AccountController(
            IAccountService accountService,
            ITokenEndpoint tokenEndpoint,
            ILogger<AccountController> logger,
            IMerchantService merchantService
        )
        {
            this._accountService = accountService;
            this._tokenEndpoint = tokenEndpoint;
            this._logger = logger;
            this._merchantService = merchantService;
        }

        /// <summary>
        /// Authenticate the credentials provided by the user
        /// </summary>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Authenticate([FromBody] AccountCredentialsRequestDto request)
        {
            // check if password is valid
            var isValid = await _accountService.Authenticate(request);
            if (!isValid)
            {
                _logger.LogError($"User {request.Username}|{request.Password} attempted to login");
                return NotFound(Results.NotFound(new { message = "Invalid Credentials" }));
            }

            // create a JWT Token based on account
            var response = await _tokenEndpoint.Connect(new ApiAccountCredentialsRequestDto
            {
                Username = request.Username,
                Password = request.Password
            });

            return Ok(Results.Ok(response));
        }

        [HttpPost("forgot-password")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ForgotPassword([FromBody] AccountMerchantRegistrationRequestDto request)
        {
            return Ok();
        }

        [HttpPost("merchant/registration")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> MerchantRegistration([FromBody] AccountMerchantRegistrationRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _merchantService.CreateMerchantAccount(request);

            return Ok();
        }

        [HttpPost("reseller/registration")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ResellerRegistration([FromBody] AccountMerchantRegistrationRequestDto request)
        {
            return Ok();
        }

        [HttpPost("customer/registration")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CustomerRegistration([FromBody] AccountMerchantRegistrationRequestDto request)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsernameClaim()
        {
            return Ok(Username);
        }

        [HttpPost("validate/website")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ValidateWebsite(string websiteUrl)
        {
            HttpClient client = new HttpClient();
            var checkingResponse = await client.GetAsync(websiteUrl);
            if (checkingResponse.IsSuccessStatusCode)
            {
                return Ok("Website is up and running.");
            }
            else
            {
                return NotFound("Website not found.");
            }
        }
    }
}
