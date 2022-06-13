using System;
using AutoMapper;
using CrossCutting.Mappings;

namespace Tests.Service
{
    public abstract class BaseServiceTest : BaseTest
    {
        protected readonly IMapper _mapper;

        protected readonly string _fakerName = Faker.Name.FullName();

        protected readonly DateTime _fakerDate = DateTime.Now;

        public BaseServiceTest() => _mapper = GetMapper();

        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            return config.CreateMapper();
        }
    }
}
