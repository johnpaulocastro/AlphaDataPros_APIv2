using Microsoft.AspNetCore.Mvc;

namespace ADPv2.Controllers.Customer
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("customer")]
    public class DashboardController : Controller
    {
        [HttpGet("dashboard")]
        public async Task GetDashboard(string username)
        {

        }

        [HttpGet]
        public async Task GenerateCode()
        {

        }
    }
}
