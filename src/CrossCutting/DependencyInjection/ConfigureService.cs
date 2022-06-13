using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using System.Diagnostics.CodeAnalysis;

namespace CrossCutting.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection service)
        {
            service.AddTransient<ILoginService, LoginService>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IWalletTypeService, WalletTypeService>();
            service.AddTransient<IWalletService, WalletService>();
            service.AddTransient<ICategoryService, CategoryService>();
            service.AddTransient<IEntranceService, EntranceService>();
        }
    }
}
