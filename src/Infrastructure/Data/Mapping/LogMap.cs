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
                .HasColumnType("VARCHAR(255)")
                .IsRequired(false);

            builder.Property(l => l.MessageTemplate)
                .HasColumnType("VARCHAR(255)")
                .IsRequired(false);

            builder.Property(l => l.Level)
                .HasColumnType("VARCHAR(255)")
                .IsRequired(false);

            builder.Property(l => l.Exception)
                .HasColumnType("VARCHAR(255)")
                .IsRequired(false);

            builder.Property(l => l.TimeStamp)
                .HasDefaultValue(DateTime.Now);

            builder.Property(l => l.Propperties)
                .HasColumnType("VARCHAR(255)")
                .IsRequired(false);
        }
    }
}
