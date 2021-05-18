using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        private IConfigurationRoot configuration;

        public MyContext CreateDbContext(string[] args)
        {
            // Add config file
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

            //SQLServer
            optionsBuilder.UseSqlServer(configuration["Database:ConnectionString"]);

            return new MyContext(optionsBuilder.Options);
        }
    }
}
