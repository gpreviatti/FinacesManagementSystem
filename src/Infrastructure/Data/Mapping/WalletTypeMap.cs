using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class WalletTypeMap : IEntityTypeConfiguration<WalletType>
    {
        public void Configure(EntityTypeBuilder<WalletType> builder)
        {
            builder.ToTable("WalletTypes");

            builder.HasKey(wt => wt.Id);

            builder.Property(wt => wt.Id)
                .HasMaxLength(36);

            builder.Property(wt => wt.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(wt => wt.Wallets)
                .WithOne(w => w.WalletType);
        }
    }
}
