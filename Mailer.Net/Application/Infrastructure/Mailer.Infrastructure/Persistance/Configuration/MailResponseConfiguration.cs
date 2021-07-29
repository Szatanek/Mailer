using Mailer.Domain.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailer.Infrastructure.Persistance.Configuration
{
    internal sealed class MailResponseConfiguration : IEntityTypeConfiguration<MailResponseEntity>
    {
        public void Configure(EntityTypeBuilder<MailResponseEntity> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.ErrorMessage);
            builder.Property(prop => prop.ErrorType);
            builder.Property(prop => prop.Timestamp);

            builder.HasOne(prop => prop.Mail)
                .WithMany(mail => mail.Responses)
                .HasForeignKey(resp => resp.MailId);
        }
    }
}