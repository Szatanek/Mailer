using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Framework.Application;
using Framework.Services.Command;
using Mailer.Api.Models;
using Mailer.Services.Contracts.Read.Queries;
using Mailer.Services.Contracts.Read.Views;
using Mailer.Services.Contracts.Write;
using Microsoft.AspNetCore.Mvc;

namespace Mailer.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public sealed class MailController : ControllerBase
    {
        private readonly IApplicationCommandBus commandBus;
        private readonly IApplicationQueryDispatcher queryDispatcher;

        public MailController(IApplicationCommandBus commandBus, IApplicationQueryDispatcher queryDispatcher)
        {
            this.commandBus = commandBus;
            this.queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> SendMailAsync(SendMailCommand command, CancellationToken cancellationToken)
        {
            await commandBus.HandleAsync(command, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MailReadViewModel>>>GetMailsAsync([FromQuery] GetMailsParameters parameters, CancellationToken cancellationToken)
        {
            var query = new GetMailsQuery(parameters.SystemId);
            var results = await queryDispatcher.DispatchAsync<GetMailsQuery, IEnumerable<MailReadViewModel>>(query, cancellationToken);
            return Ok(results);
        }
    }
}
