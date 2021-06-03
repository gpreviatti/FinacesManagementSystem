using System;
using System.Collections.Generic;
using Domain.Entities;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeds
{
    public class UserSeeder
    {
        public static void Users(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new List<User>()
                {
                    new User {
                        Id = Guid.Parse("430E0144-289F-4A95-8F14-BACFABB3FE8A"),
                        Name = "Admin",
                        Email = "admin@admin.com",
                        Password = EncryptHelper.HashField("mudar@123")
                    },
                    new User {
                        Id = Guid.Parse("CB43D078-87F1-4864-853A-E626922B8109"),
                        Name = "Test-User-01",
                        Email = "testUser01@email.com",
                        Password = EncryptHelper.HashField("mudar@123")
                    }
                }
            );
        }
    }
}
