using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class WalletMap : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("Wallets");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                .HasMaxLength(36);

            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(w => w.Description)
                .HasMaxLength(500);

            builder.Property(w => w.CurrentValue)
                .HasDefaultValue(0);

            builder.HasOne(w => w.User)
                .WithMany(u => u.Wallets);

            builder.HasOne(w => w.WalletType)
                .WithMany(wt => wt.Wallets);

            builder.HasMany(w => w.Entraces)
                .WithOne(e => e.Wallet);
        }
    }
}
