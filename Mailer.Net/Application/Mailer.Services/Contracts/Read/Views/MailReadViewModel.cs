using System;

namespace Mailer.Services.Contracts.Read.Views
{
    public sealed class MailReadViewModel
    {
        public Guid Guid { get; set; }

        public string Topic { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Status { get; set; }

        public DateTime Timestamp { get; set; }
    }
}