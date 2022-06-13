using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Data.Mapping
{
    [ExcludeFromCodeCoverage]
    public class UserMap : BaseMap<User>
    {
        public UserMap() : base("Users") { }

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255);

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
