using ADPv2.ClassHelpers;
using Microsoft.AspNetCore.Mvc;

namespace ADPv2.AdminControllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("admin")]
    public class AdminController : Controller
    {
        [HttpGet("encrypt")]
        public async Task<IActionResult> EncryptData(string dataToEncrypt)
        {
            var result = Base64Helper.EncryptToBase64(dataToEncrypt);
            return Ok(result);
        }

        [HttpGet("decrypt")]
        public async Task<IActionResult> DecryptDate(string dataToDecrypt)
        {
            var result = Base64Helper.DecryptFromBase64(dataToDecrypt);
            return Ok(result);
        }
    }
}
