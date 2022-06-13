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
            CreateMap<UserCreateDto, User>()
                .ReverseMap();
            // This option will prevent AutoMapper to replace fields that not will be updated to null
            CreateMap<UserUpdateDto, User>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<User, UserResultDto>().ReverseMap();

            // WalletType
            //CreateMap<WalletTypeCreateDto, WalletType>()
            //    .ReverseMap();
            //// This option will prevent AutoMapper to replace fields that not will be updated to null
            //CreateMap<WalletTypeUpdateDto, WalletType>()
            //    .ReverseMap()
            //    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            //CreateMap<WalletType, WalletTypeResultDto>().ReverseMap();
        }
    }
}
