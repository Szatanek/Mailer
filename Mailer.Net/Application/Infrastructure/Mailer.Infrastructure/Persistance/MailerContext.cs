using System.Reflection;
using Mailer.Domain.Mail;
using Mailer.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Mailer.Infrastructure.Persistance
{
    public sealed class MailerContext : DbContext
    {
        public MailerContext(DbContextOptions<MailerContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; private set; }

        public DbSet<MailEntity> Mails { get; private set; }

        private DbSet<MailResponseEntity> MailResponses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("mailer");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
