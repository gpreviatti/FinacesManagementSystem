using System;
using System.Collections.Generic;
using Domain.Entities;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeds
{
    public class WalletTypeSeeder
    {
        public static void WalletTypes(ModelBuilder modelBuilder)
        {
            var walletTypes = new List<WalletType>();

            walletTypes.Add(CreateWalletType("Checking Account"));
            walletTypes.Add(CreateWalletType("Credit"));
            walletTypes.Add(CreateWalletType("Saving"));
            walletTypes.Add(CreateWalletType("Investiments"));
            walletTypes.Add(CreateWalletType("Stocks"));

            modelBuilder.Entity<WalletType>().HasData(walletTypes);
        }

        public static WalletType CreateWalletType(string name)
        {
            return new WalletType()
            {
                Id = new Guid(),
                Name = name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
