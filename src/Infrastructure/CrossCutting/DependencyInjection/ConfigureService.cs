using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection service)
        {
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IWalletTypeService, WalletTypeService>();
            service.AddTransient<IWalletService, WalletService>();
            service.AddTransient<ICategoryService, CategoryService>();
            service.AddTransient<IEntranceService, EntranceService>();
        }
    }
}
