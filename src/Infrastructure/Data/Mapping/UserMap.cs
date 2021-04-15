using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasMaxLength(36);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired();

            builder.HasMany(u => u.Wallets)
                .WithOne(w => w.User);

            builder.HasMany(u => u.Categories)
                .WithOne(c => c.User);
        }
    }
}
