using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApiRest.Models.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(100);

            builder.HasAlternateKey(x => x.Email)
                .HasName("ix_unique_email");

            builder.Property(x => x.Password)
                .HasColumnName("password")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PasswordSalt)
                .HasColumnName("passwordsalt")
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}