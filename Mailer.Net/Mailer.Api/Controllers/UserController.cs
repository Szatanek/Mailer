using Mailer.Services;
using Mailer.Services.Contracts.Read.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Mailer.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public sealed class UserController : ControllerBase
    {
        private readonly UserService service;

        public UserController(UserService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("logIn")]
        public IActionResult GetUserDetails([FromBody] GetUserDetailsQuery query)
        {
            var result = service.GetUserDetails(query);
            return Ok(result);
        }
    }
}
