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
                    new User() {
                        Id = new Guid("37e4f662-1987-4b9d-b245-ad6014f3217a"),
                        Name = "Admin",
                        Email = "admin@admin.com",
                        Password = EncryptHelper.HashField("mudar@123"),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new User() {
                        Id = new Guid("6823c5ae-ca43-4287-82e1-10d5fca46a2e"),
                        Name = "Test-User-01",
                        Email = "testUser01@email.com",
                        Password = EncryptHelper.HashField("mudar@123"),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                }
            );
        }
    }
}
