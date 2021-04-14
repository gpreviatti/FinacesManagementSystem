using AutoMapper;
using Domain.Dtos.Category;
using Domain.Dtos.Entrace;
using Domain.Dtos.User;
using Domain.Dtos.Wallet;
using Domain.Dtos.WalletType;
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

            // Wallet
            CreateMap<WalletCreateDto, Wallet>();
            // This option will prevent AutoMapper to replace fields that not will be updated to null
            CreateMap<WalletUpdateDto, Wallet>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Wallet, WalletResultDto>().ReverseMap();

            // WalletType
            CreateMap<WalletTypeCreateDto, WalletType>();
            // This option will prevent AutoMapper to replace fields that not will be updated to null
            CreateMap<WalletTypeUpdateDto, WalletType>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<WalletType, WalletTypeResultDto>().ReverseMap();

            // Category
            CreateMap<CategoryCreateDto, Category>();
            // This option will prevent AutoMapper to replace fields that not will be updated to null
            CreateMap<CategoryUpdateDto, Category>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Category, CategoryResultDto>().ReverseMap();

            // Entrace
            CreateMap<EntraceCreateDto, Entrace>();
            // This option will prevent AutoMapper to replace fields that not will be updated to null
            CreateMap<EntraceUpdateDto, Entrace>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Entrace, EntraceResultDto>().ReverseMap();
        }
    }
}
