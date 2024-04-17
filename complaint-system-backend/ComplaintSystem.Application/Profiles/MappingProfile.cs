using AutoMapper;
using ComplaintSystem.Domain.Entities;
using ComplaintSystem.Application.DTOs.UserDto;


namespace ComplaintSystem.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Profile
            CreateMap<UserEntity, CreateUserDto>().ReverseMap();
            CreateMap<UserEntity, GetUserDto>().ReverseMap();

        }
    }
}