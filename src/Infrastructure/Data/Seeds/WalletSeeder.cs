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
            walletTypes.Add(CreateWallet("Inter", "Main Account", 500, WalletTypeHelper.CheckingAccount));
            walletTypes.Add(CreateWallet("Credit", "Credit Card Account", 500, WalletTypeHelper.Credit));
            //walletTypes.Add(CreateWallet("Saving"));
            //walletTypes.Add(CreateWallet("Investiments"));
            //walletTypes.Add(CreateWallet("Stocks"));

            modelBuilder.Entity<Wallet>().HasData(walletTypes);
        }

        public static Wallet CreateWallet(string name, string description, double currentValue, Guid walletTypeId)
        {
            return new Wallet()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                CurrentValue = currentValue,
                CloseDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(15),
                WalletTypeId = walletTypeId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
