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

            walletTypes.Add(CreateWalletType("Checking Account", "AD4AC47F-0888-4D60-81F9-964153B13E37"));
            walletTypes.Add(CreateWalletType("Credit", "AD4AC47F-0888-4D60-81F9-964153B13E38"));
            walletTypes.Add(CreateWalletType("Saving", "AD4AC47F-0888-4D60-81F9-964153B13E39"));
            walletTypes.Add(CreateWalletType("Investiments", "AD4AC47F-0888-4D60-81F9-964153B13E40"));
            walletTypes.Add(CreateWalletType("Stocks", "AD4AC47F-0888-4D60-81F9-964153B13E41"));

            modelBuilder.Entity<WalletType>().HasData(walletTypes);
        }

        public static WalletType CreateWalletType(string name, string guid)
        {
            return new WalletType()
            {
                Id = Guid.Parse(guid),
                Name = name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
