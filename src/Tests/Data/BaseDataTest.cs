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
        protected readonly IServiceProvider _serviceProvider;
        protected readonly MyContext _context;

        public BaseDataTest()
        {
            // Add config file
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var filename = Directory.GetCurrentDirectory() + $"/../../Web/appsettings.{environment}.json";

            // Add config file
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(filename)
                .Build();

            var serviceCollection = new ServiceCollection();

            // In Memory
            serviceCollection.AddDbContext<MyContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("App"))
            );

            _serviceProvider = serviceCollection.BuildServiceProvider();
            _context = _serviceProvider.GetService<MyContext>();
            _context.Database.EnsureCreated();
        }
    }
}
