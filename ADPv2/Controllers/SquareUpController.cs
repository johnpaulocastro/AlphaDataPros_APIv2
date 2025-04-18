using ADPv2.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Square.Models;

namespace ADPv2.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("card-payment")]
    public class SquareUpController : BaseController
    {
        private readonly ISquareUpService _squareUpService;

        public SquareUpController(ISquareUpService squareUpService)
        {
            _squareUpService = squareUpService;
        }

        [HttpGet("CreatePayment")]
        public async Task<IActionResult> CreatePayment()
        {
            var squareToken = await _squareUpService.GetToken();
            return Ok(squareToken);
        }
    }
}
