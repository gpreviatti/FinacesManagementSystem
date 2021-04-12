using AutoMapper;
using Domain.Dtos.User;
using Domain.Entities;

namespace CrossCutting.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<UserCreateDto, User>();
            // This option will prevent AutoMapper to replace fields that not will be updated to null
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<User, UserResultDto>().ReverseMap();
        }
    }
}
