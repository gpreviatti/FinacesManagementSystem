using AutoMapper;
using CrossCutting.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureAutoMapper
    {
        public static void ConfigureDepencenciesAutoMapper(IServiceCollection service)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            IMapper mapper = config.CreateMapper();
            service.AddSingleton(mapper);
        }
    }
}
