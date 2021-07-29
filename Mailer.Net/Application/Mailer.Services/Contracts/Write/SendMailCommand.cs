using System.Collections.Generic;
using Framework.Application;

namespace Mailer.Services.Contracts.Write
{
    public sealed class SendMailCommand : ApplicationCommand
    {
        public string Topic { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        public bool IsHtml { get; set; }
        public int SystemId { get; set; }
        public IEnumerable<string> Recipients { get; set; }
    }
}