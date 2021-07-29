namespace Mailer.Services.Contracts.Read.Queries
{
    public sealed class GetUserDetailsQuery
    {
        public string Login { get; set; }
        public string Pin { get; set; }
    }
}