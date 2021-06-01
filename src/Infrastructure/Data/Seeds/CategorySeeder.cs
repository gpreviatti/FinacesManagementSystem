using System;
using System.Collections.Generic;
using Domain.Entities;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeds
{
    public class CategorySeeder
    {
        public static void Categories(ModelBuilder modelBuilder)
        {
            var categories = new List<Category>();

            #region Incomes
            categories.Add(CreateCategory("Salary", "18301DE4-A10F-404E-8CE7-836F297382BB"));
            categories.Add(CreateCategory("Loans", "18341DE4-A10F-404E-8CE7-836F297382BB"));
            categories.Add(CreateCategory("Other Earnings", "18351DE4-A10F-404E-8CE7-836F297382BB"));
            categories.Add(CreateCategory("Investiments", "18361DE4-A10F-404E-8CE7-836F297382BB"));
            #endregion

            #region Outcomes
            categories.Add(CreateCategory("Food", "18371DE4-A10F-404E-8CE7-836F297382BB"));
            categories.Add(CreateCategory("Transport", "18381DE4-A10F-404E-8CE7-836F297382BB"));
            categories.Add(CreateCategory("Services", "18391DE4-A10F-404E-8CE7-836F297382BB"));
            categories.Add(CreateCategory("Health", "18401DE4-A10F-404E-8CE7-836F297382BB"));
            categories.Add(CreateCategory("Education", "18411DE4-A10F-404E-8CE7-836F297382BB"));
            categories.Add(CreateCategory("Travel", "18421DE4-A10F-404E-8CE7-836F297382BB"));
            categories.Add(CreateCategory("Work", "18431DE4-A10F-404E-8CE7-836F297382BB"));
            categories.Add(CreateCategory("Gifts", "18441DE4-A10F-404E-8CE7-836F297382BB"));
            categories.Add(CreateCategory("Home", "18451DE4-A10F-404E-8CE7-836F297382BB"));
            categories.Add(CreateCategory("Other Expanses", "18461DE4-A10F-404E-8CE7-836F297382BB"));
            #endregion

            modelBuilder.Entity<Category>().HasData(categories);
        }

        public static Category CreateCategory(string name, string id = "")
        {
            return new Category()
            {
                Id = string.IsNullOrEmpty(id) ? Guid.NewGuid() : Guid.Parse(id),
                Name = name
            };
        }
    }
}
