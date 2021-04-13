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
            categories.Add(CreateCategory("Salary"));
            categories.Add(CreateCategory("Loans"));
            categories.Add(CreateCategory("Other Earnings"));
            categories.Add(CreateCategory("Investiments"));
            #endregion

            #region Outcomes
            categories.Add(CreateCategory("Food"));
            categories.Add(CreateCategory("Transport"));
            categories.Add(CreateCategory("Services"));
            categories.Add(CreateCategory("Health"));
            categories.Add(CreateCategory("Education"));
            categories.Add(CreateCategory("Travel"));
            categories.Add(CreateCategory("Work"));
            categories.Add(CreateCategory("Gifts"));
            #endregion

            #region Investiments
            categories.Add(CreateCategory("Industrials"));
            categories.Add(CreateCategory("Financials"));
            categories.Add(CreateCategory("Energy"));
            categories.Add(CreateCategory("Consumer Discretionary"));
            categories.Add(CreateCategory("Information Technology"));
            categories.Add(CreateCategory("Communication Services"));
            categories.Add(CreateCategory("Real Estate"));
            categories.Add(CreateCategory("Health Care"));
            categories.Add(CreateCategory("Consumer Staples"));
            categories.Add(CreateCategory("Utilities"));
            #endregion

            modelBuilder.Entity<User>().HasData(categories);
        }

        public static Category CreateCategory(string name)
        {
            return new Category()
            {
                Id = new Guid(),
                Name = name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
