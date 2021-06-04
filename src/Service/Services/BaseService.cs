using AutoMapper;
using Domain.Interfaces.Services;

namespace Service.Services
{
    public abstract class BaseService : IBaseService
    {
        protected IMapper _mapper;
    }
}
