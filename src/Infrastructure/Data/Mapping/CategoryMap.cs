using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasMaxLength(36);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(c => c.UserId)
                .IsRequired();
            builder.HasOne(c => c.User);
        }
    }
}
