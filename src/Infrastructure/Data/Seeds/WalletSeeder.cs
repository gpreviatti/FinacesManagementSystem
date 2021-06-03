using System;
using System.Collections.Generic;
using Domain.Entities;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeds
{
    public class WalletSeeder
    {
        public static void Wallet(ModelBuilder modelBuilder)
        {
            var walletTypes = new List<Wallet>();
            walletTypes.Add(CreateWallet("Main Card", "Main Account", WalletTypeHelper.CheckingAccount, "040CC3AD-2159-4B8E-894E-E700A121B48F"));
            walletTypes.Add(CreateWallet("Credit", "Credit Card Account", WalletTypeHelper.Credit, "041CC3AD-2159-4B8E-894E-E700A121B48F"));
            walletTypes.Add(CreateWallet("Saving", "My Savings", WalletTypeHelper.Saving, "042CC3AD-2159-4B8E-894E-E700A121B48F"));

            modelBuilder.Entity<Wallet>().HasData(walletTypes);
        }

        public static Wallet CreateWallet(string name, string description, Guid walletTypeId, string id)
        {
            return new Wallet
            {
                Id = Guid.Parse(id),
                Name = name,
                Description = description,
                UserId = Guid.Parse("430E0144-289F-4A95-8F14-BACFABB3FE8A"),
                WalletTypeId = walletTypeId,
                CloseDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(15)
            };
        }
    }
}
