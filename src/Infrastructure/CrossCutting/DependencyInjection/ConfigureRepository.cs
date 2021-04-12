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


            var dbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION");

            // MySql
            //serviceCollection.AddDbContext<MyContext>(options => options.UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection)));

            // SqlServer
            serviceCollection.AddDbContext<MyContext>(options => options.UseSqlServer(dbConnection));
        }
    }
}
