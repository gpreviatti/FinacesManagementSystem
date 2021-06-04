using System.IO;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Data
{
    public abstract class BaseDataTest : BaseTest
    {
        protected MyContext _context;

        public BaseDataTest()
        {
            var filename = Directory.GetCurrentDirectory() + $"/../../../../Web/appsettings.{_environment}.json";

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(filename)
                .Build();

            SetupDatabase(configuration);
        }

        /// <summary>
        /// Setup Database connection and context
        /// </summary>
        /// <param name="configuration"></param>
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
