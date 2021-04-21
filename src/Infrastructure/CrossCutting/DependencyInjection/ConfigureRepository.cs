using Data.Context;
using Data.Repositories;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IWalletTypeRepository, WalletTypeRepository>();
            serviceCollection.AddScoped<IWalletRepository, WalletRepository>();
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddScoped<IEntraceRepository, EntraceRepository>();

            //var dbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION");

            // SqlServer
            //serviceCollection.AddDbContext<MyContext>(options => options.UseSqlServer(dbConnection));

            serviceCollection.AddDbContext<MyContext>(
                options => options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=FmsDB")
            );
        }
    }
}
