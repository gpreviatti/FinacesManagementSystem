using Domain;
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
                .HasMaxLength(60);

            builder.Property(w => w.Description)
                .HasMaxLength(255);

            builder.Property(w => w.CurrentValue)
                .HasDefaultValue(0);

            builder.HasOne(w => w.WalletType);

            builder.HasMany(w => w.Entraces);
        }
    }
}
