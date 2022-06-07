using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MyContext>();

            Environment.SetEnvironmentVariable(
                "DB_CONNECTION", 
                "Host=localhost;Port=5432;Database=FinancesManagementSystem;User ID=postgres;Password=admin"
            );

            builder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION"));

            return new MyContext(builder.Options);
        }
    }
}
