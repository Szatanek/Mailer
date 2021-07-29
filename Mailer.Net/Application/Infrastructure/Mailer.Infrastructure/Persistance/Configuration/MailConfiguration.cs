using System;
using Mailer.Domain.Mail;
using Mailer.Domain.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailer.Infrastructure.Persistance.Configuration
{
    internal sealed class MailConfiguration : IEntityTypeConfiguration<MailEntity>
    {
        public void Configure(EntityTypeBuilder<MailEntity> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.HasIndex(prop => prop.MailGuid);

            builder.Property(prop => prop.MailGuid)
                .HasConversion(
                    mailId => (Guid)mailId,
                    value => (MailGuid)value);

            builder.Property(prop => prop.Topic)
                .HasConversion(
                    topic => (string)topic,
                    value => (MailTopic)value)
                .HasMaxLength(250);

            builder.Property(prop => prop.Sender)
                .HasConversion(
                    sender => (string)sender,
                    value => (MailAddress)value)
                .HasMaxLength(50);

            builder.Property(prop => prop.Recipient)
                .HasConversion(
                    recipient => (string)recipient,
                    value => (MailRecipient)value)
                .HasMaxLength(250);

            builder.Property(prop => prop.Status)
                .HasConversion(
                    status => (byte)status,
                    value => (MailStatus)value);

            builder.Property(prop => prop.SystemId)
                .HasConversion(
                    status => (int)status,
                    value => (SystemId)value);

            builder.OwnsOne(x => x.Body)
                .Property(b => b.Content)
                .HasColumnName("Content");

            builder.OwnsOne(x => x.Body)
                .Property(b => b.IsHtml)
                .HasColumnName("IsHtml");

            builder.HasMany(x => x.Responses)
                .WithOne(x => x.Mail)
                .HasForeignKey(x => x.MailId);
        }
    }
}