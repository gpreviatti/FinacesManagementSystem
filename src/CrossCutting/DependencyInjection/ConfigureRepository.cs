using System;
using System.Diagnostics.CodeAnalysis;
using Data.Context;
using Data.Repositories;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection service)
        {
            // Add Dependency Injection for repositories below
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IWalletTypeRepository, WalletTypeRepository>();
            service.AddScoped<IWalletRepository, WalletRepository>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IEntranceRepository, EntranceRepository>();

            // Connection Configs
            service.AddDbContext<MyContext>(
                options => options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION"))
            );
        }
    }
}
