using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IWalletTypeService, WalletTypeService>();
            serviceCollection.AddTransient<IWalletService, WalletService>();
            serviceCollection.AddTransient<ICategoryService, CategoryService>();
            serviceCollection.AddTransient<IEntranceService, EntranceService>();
        }
    }
}
