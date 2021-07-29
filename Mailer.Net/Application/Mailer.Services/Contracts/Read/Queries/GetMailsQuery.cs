using Framework.Application;

namespace Mailer.Services.Contracts.Read.Queries
{
    public sealed class GetMailsQuery : ApplicationQuery
    {
        public GetMailsQuery(int systemId)
            : base()
        {
            SystemId = systemId;
        }

        public int SystemId { get; }
    }
}