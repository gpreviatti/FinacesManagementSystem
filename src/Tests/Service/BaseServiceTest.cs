using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CrossCutting.Mappings;
using Tests.AutoMapper;

namespace Tests.Service
{
    public abstract class BaseServiceTest
    {
        protected readonly IMapper _mapper;

        public BaseServiceTest()
        {
            _mapper = GetMapper();
        }

        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            return config.CreateMapper();
        }
    }
}
