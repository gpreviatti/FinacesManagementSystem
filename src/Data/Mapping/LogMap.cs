using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Message)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(l => l.MessageTemplate)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(l => l.Level)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(l => l.Exception)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(l => l.TimeStamp)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValue(DateTime.Now);

            builder.Property(l => l.Propperties)
                .HasMaxLength(100)
                .IsRequired(false);
        }
    }
}
