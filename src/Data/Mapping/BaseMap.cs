using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        private readonly string _tableName;

        public BaseMap(string tableName = "") => _tableName = tableName;

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            if (!string.IsNullOrEmpty(_tableName))
                builder.ToTable(_tableName);

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasMaxLength(36);

            builder.Property(x => x.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValue(DateTime.Now);
            
            builder.Property(x => x.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValue(DateTime.Now);
        }
    }
}
