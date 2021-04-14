using System;
using System.Collections.Generic;
using Domain.Entities;
using Helpers.Enums;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeds
{
    public class WalletTypeSeeder
    {
        public static void WalletTypes(ModelBuilder modelBuilder)
        {
            var walletTypes = new List<WalletType>();

            walletTypes.Add(CreateWalletType("Checking Account", EWalletType.CheckingAccount.Guid()));
            walletTypes.Add(CreateWalletType("Credit", EWalletType.Credit.Guid()));
            walletTypes.Add(CreateWalletType("Saving", EWalletType.Saving.Guid()));
            walletTypes.Add(CreateWalletType("Investiments", EWalletType.Investiments.Guid()));
            walletTypes.Add(CreateWalletType("Stocks", EWalletType.Stocks.Guid()));

            modelBuilder.Entity<WalletType>().HasData(walletTypes);
        }

        public static WalletType CreateWalletType(string name, Guid guid)
        {
            return new WalletType()
            {
                Id = guid,
                Name = name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
