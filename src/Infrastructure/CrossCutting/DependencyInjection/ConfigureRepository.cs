using Data.Context;
using Data.Repositories;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection service, IConfiguration configuration)
        {
            // Add Dependency Injection for repositories below
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IWalletTypeRepository, WalletTypeRepository>();
            service.AddScoped<IWalletRepository, WalletRepository>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IEntranceRepository, EntranceRepository>();

            // Connection Configs
            service.AddDbContext<MyContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("App"))
            );
        }
    }
}
