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
        private IConfigurationRoot configuration;

        public BaseDataTest()
        {
            var serviceCollection = new ServiceCollection();

            // Add config file
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // In Memory
            serviceCollection.AddDbContext<MyContext>(options => options.UseSqlServer(configuration["Database:ConnectionString"]));

            _serviceProvider = serviceCollection.BuildServiceProvider();
            _context = _serviceProvider.GetService<MyContext>();
            _context.Database.EnsureCreated();
        }
    }
}
