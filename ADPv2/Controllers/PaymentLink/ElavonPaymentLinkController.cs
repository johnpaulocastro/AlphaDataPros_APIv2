using ADPv2.Client;
using Microsoft.AspNetCore.Mvc;

namespace ADPv2.Controllers.PaymentLink
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("convergepay")]
    [ApiController]
    public class ElavonPaymentLinkController : Controller
    {
        private readonly IElavonConvergeClient _convergeClient;
        private readonly ILogger<ElavonPaymentLinkController> _logger;

        public ElavonPaymentLinkController(IElavonConvergeClient convergeClient,
            ILogger<ElavonPaymentLinkController> logger)
        {
            _convergeClient = convergeClient;
            _logger = logger;
        }

        [HttpGet("tansaction_token")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTransactionToken()
        {
            var response = await _convergeClient.GetSessionToken();
            return Ok(response);
        }
    }
}
