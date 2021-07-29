using Mailer.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailer.Infrastructure.Persistance.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(prop => prop.Login).HasColumnName("Login")
                .HasConversion(
                    login => (string)login,
                    value => (UserLogin)value);

            builder.Property(prop => prop.Id).HasColumnName("Id");

            builder.OwnsOne(
                x => x.Name,
                name =>
                {
                    name.Property(n => n.FirstName)
                        .HasColumnName("FirstName");
                    name.Property(n => n.LastName)
                        .HasColumnName("LastName");
                });

            builder.HasIndex(prop => prop.Id);
        }
    }
}