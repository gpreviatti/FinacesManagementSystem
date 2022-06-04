using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class CategoryMap : BaseMap<Category>
    {
        public CategoryMap() : base("Categories") { }

        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(c => c.Entrances)
                .WithOne(e => e.Category);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Categories);

            builder.HasOne(c => c.CustomCategory)
                .WithMany();
        }
    }
}