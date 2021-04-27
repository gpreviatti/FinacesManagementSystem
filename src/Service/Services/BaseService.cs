using System;
using AutoMapper;
using Domain.Interfaces.Services;

namespace Service.Services
{
    public abstract class BaseService : IBaseService
    {
        protected IMapper _mapper;
        protected Guid UserId { get; set; }

        public BaseService()
        {
            UserId = Guid.Parse("430E0144-289F-4A95-8F14-BACFABB3FE8A");
        }
    }
}
