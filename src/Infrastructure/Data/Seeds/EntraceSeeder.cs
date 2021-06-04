using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeds
{
    public class EntraceSeeder
    {
        public static void Entrance(ModelBuilder modelBuilder)
        {
            var random = new Random();
            var entrances = new List<Entrance>();

            var categoriesGuid = new List<string> {
                "18371DE4-A10F-404E-8CE7-836F297382BB",
                "18381DE4-A10F-404E-8CE7-836F297382BB",
                "18391DE4-A10F-404E-8CE7-836F297382BB",
                "18401DE4-A10F-404E-8CE7-836F297382BB",
                "18411DE4-A10F-404E-8CE7-836F297382BB",
                "18421DE4-A10F-404E-8CE7-836F297382BB",
                "18431DE4-A10F-404E-8CE7-836F297382BB",
                "18441DE4-A10F-404E-8CE7-836F297382BB",
                "18451DE4-A10F-404E-8CE7-836F297382BB",
                "18461DE4-A10F-404E-8CE7-836F297382BB"
            };

            var walletsGuid = new List<string>
            {
                "040CC3AD-2159-4B8E-894E-E700A121B48F",
                "041CC3AD-2159-4B8E-894E-E700A121B48F",
                "042CC3AD-2159-4B8E-894E-E700A121B48F"
            };

            for (int i = 1; i <= 20000; i++)
            {
                var randonCategory = random.Next(categoriesGuid.Count());
                var randonWallet = random.Next(walletsGuid.Count());
                entrances.Add(new Entrance
                {
                    Id = Guid.NewGuid(),
                    Description = "Lorem Ipsum",
                    Observation = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                    Type = random.Next(1, 3),
                    Value = random.Next(100, 1000),
                    CategoryId = Guid.Parse(categoriesGuid[randonCategory]),
                    WalletId = Guid.Parse(walletsGuid[randonWallet])
                });
            }
            modelBuilder.Entity<Entrance>().HasData(entrances);
        }
    }
}
