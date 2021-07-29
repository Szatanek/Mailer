using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mailer.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public sealed class HealthCheckController : ControllerBase
    {
        [HttpGet("simple")]
        public IActionResult SimpleHealthCheck() 
        {
            const string result = "Simple result OK";
            return Ok(result);
        }

        [HttpGet("async")]
        public async Task<IActionResult> SimpleHealthCheck(CancellationToken cancellationToken)
        {
            await Task.Delay(100);
            var result = await Task.FromResult("Async result OK");
            return Ok(result);
        }
    }
}
