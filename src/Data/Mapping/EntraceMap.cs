using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class EntranceMap : BaseMap<Entrance>
    {
        public EntranceMap() : base("Entrances") { }

        public override void Configure(EntityTypeBuilder<Entrance> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Observation)
                .HasMaxLength(500);

            builder.Property(e => e.Type)
                .IsRequired();

            builder.Property(e => e.Ticker)
                .HasMaxLength(10);

            builder.Property(e => e.Value)
                .IsRequired();

            builder.HasOne(e => e.Wallet)
                .WithMany(w => w.Entrances)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(e => new { e.Id, e.CategoryId })
                .IsClustered(false);

            builder
                .HasOne(e => e.Category)
                .WithMany(c => c.Entrances);
        }
    }
}
