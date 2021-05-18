using AutoMapper;
using CrossCutting.Mappings;

namespace Tests.AutoMapper
{
    public class BaseMapperTest
    {
        protected readonly IMapper _mapper;
        public BaseMapperTest()
        {
            _mapper = GetMapper();
        }

        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));

            return config.CreateMapper();
        }
    }
}
