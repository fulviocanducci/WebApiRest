using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApiRest.Models.Mappings
{
    public class TodoMapping : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("todos");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(100)
                .IsRequired();               

            builder.Property(x => x.Done)
                .HasColumnName("done") 
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}