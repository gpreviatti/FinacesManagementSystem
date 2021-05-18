using System;
using AutoMapper;
using CrossCutting.Mappings;

namespace Tests.Service
{
    public abstract class BaseServiceTest
    {
        protected readonly IMapper _mapper;

        public string FakerName { get; set; }

        public DateTime FakerDate { get; set; } = DateTime.Now;

        public BaseServiceTest()
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
