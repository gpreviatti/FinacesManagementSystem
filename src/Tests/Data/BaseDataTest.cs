using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Data
{
    public abstract class BaseDataTest : IDisposable
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly MyContext _context;

        public BaseDataTest()
        {
            var serviceCollection = new ServiceCollection();

            // In Memory
            serviceCollection.AddDbContext<MyContext>(
                options => options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=FmsDBDataTests")
            );

            _serviceProvider = serviceCollection.BuildServiceProvider();
            _context = _serviceProvider.GetService<MyContext>();
            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
