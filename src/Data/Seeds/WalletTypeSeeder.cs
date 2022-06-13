using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeds
{
    [ExcludeFromCodeCoverage]
    public class WalletTypeSeeder
    {
        public static void WalletTypes(ModelBuilder modelBuilder)
        {
            var walletTypes = new List<WalletType>();

            walletTypes.Add(CreateWalletType("Checking Account", WalletTypeHelper.CheckingAccount));
            walletTypes.Add(CreateWalletType("Credit", WalletTypeHelper.Credit));
            walletTypes.Add(CreateWalletType("Saving", WalletTypeHelper.Saving));
            walletTypes.Add(CreateWalletType("Investiments", WalletTypeHelper.Investiments));
            walletTypes.Add(CreateWalletType("Stocks", WalletTypeHelper.Stocks));

            modelBuilder.Entity<WalletType>().HasData(walletTypes);
        }

        public static WalletType CreateWalletType(string name, Guid guid)
        {
            return new WalletType { Id = guid, Name = name };
        }
    }
}
