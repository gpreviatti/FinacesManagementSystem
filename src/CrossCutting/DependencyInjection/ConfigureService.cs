using Domain.Interfaces.Services;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using System.Diagnostics.CodeAnalysis;

namespace CrossCutting.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ConfigureService
{
    public static void ConfigureDependenciesService(IServiceCollection service)
    {   
        // Domain Services
        service.AddScoped<IWalletTypeService, WalletTypeService>();
        service.AddScoped<IWalletService, WalletService>();
        service.AddScoped<ICategoryService, CategoryService>();
        service.AddScoped<IEntranceService, EntranceService>();

        // Application Services
        service.AddScoped<ILoginService, LoginService>();
        service.AddScoped<IUserService, UserService>();
    }
}
