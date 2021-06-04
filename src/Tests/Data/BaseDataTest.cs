using System;
using System.IO;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Data
{
    public abstract class BaseDataTest
    {
        protected MyContext _context;

        /// <summary>
        /// Add Config file and set environment
        /// </summary>
        public BaseDataTest()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var filename = Directory.GetCurrentDirectory() + $"/../../../../Web/appsettings.{environment}.json";

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(filename)
                .Build();

            SetupDatabase(configuration);
        }

        public void SetupDatabase(IConfigurationRoot configuration)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<MyContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("App"))
            );

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _context = serviceProvider.GetService<MyContext>();
            _context.Database.EnsureCreated();
        }
    }
}
