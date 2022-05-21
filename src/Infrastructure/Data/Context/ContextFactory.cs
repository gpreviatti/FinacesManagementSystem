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
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var filename = Directory.GetCurrentDirectory() + $"/../../Web/appsettings.{environment}.json";

            // Add config file
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(filename)
                .Build();

            var builder = new DbContextOptionsBuilder<MyContext>();

            builder.UseSqlServer(configuration.GetSection("ConnectionString").Value);

            return new MyContext(builder.Options);
        }
    }
}
