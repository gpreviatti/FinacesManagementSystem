using Domain.Entities;
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
                .HasMaxLength(255);

            builder.HasMany(c => c.Entraces)
                .WithOne(e => e.Category);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Categories);

            builder.HasOne(c => c.CustomCategory)
                .WithMany();
        }
    }
}
