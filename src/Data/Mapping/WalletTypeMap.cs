using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Data.Mapping
{
    [ExcludeFromCodeCoverage]
    public class WalletTypeMap : BaseMap<WalletType>
    {
        public WalletTypeMap() : base("WalletTypes") { }
        public override void Configure(EntityTypeBuilder<WalletType> builder)
        {
            base.Configure(builder);

            builder.Property(wt => wt.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(wt => wt.Wallets)
                .WithOne(w => w.WalletType);
        }
    }
}
