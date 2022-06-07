using System;
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
            SetupDatabase();
        }

        /// <summary>
        /// Setup Database connection and context
        /// </summary>
        public void SetupDatabase()
        {
            var serviceCollection = new ServiceCollection();

            Environment.SetEnvironmentVariable(
                "DB_CONNECTION", 
                "Host=localhost;Port=5432;Database=FinancesManagementSystem;User ID=postgres;Password=admin"
            );

            serviceCollection.AddDbContext<MyContext>(
                options => options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION"))
            );

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _context = serviceProvider.GetService<MyContext>();
            _context.Database.Migrate();
        }
    }
}
