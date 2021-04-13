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

            walletTypes.Add(CreateWallet("Inter", "Main Account", 500, Guid.Parse("AD4AC47F-0888-4D60-81F9-964153B13E37")));
            //walletTypes.Add(CreateWallet("Credit"));
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
