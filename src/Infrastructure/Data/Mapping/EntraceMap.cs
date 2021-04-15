using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class EntraceMap : IEntityTypeConfiguration<Entrace>
    {
        public void Configure(EntityTypeBuilder<Entrace> builder)
        {
            builder.ToTable("Entrace");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasMaxLength(36);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Observation)
                .HasMaxLength(255);

            builder.Property(e => e.Type)
                .IsRequired();

            builder.Property(e => e.Ticker)
                .HasMaxLength(20);

            builder.Property(e => e.Value)
                .IsRequired();
        }
    }
}
