using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Data.Mapping;

[ExcludeFromCodeCoverage]
public class CategoryMap : BaseMap<Category>
{
    public CategoryMap() : base("Categories") { }

    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(c => c.Entrances)
            .WithOne(e => e.Category);

        builder.HasOne(c => c.User)
            .WithMany(u => u.Categories);

        builder.HasOne(c => c.CustomCategory)
            .WithMany();
    }
}
