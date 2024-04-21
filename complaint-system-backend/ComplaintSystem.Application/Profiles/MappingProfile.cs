using AutoMapper;
using ComplaintSystem.Domain.Entities;
using ComplaintSystem.Application.DTOs.UserDto;
using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.DTOs.ManagerDto;
using ComplaintSystem.Application.DTOs.AdminDto;
using ComplaintSystem.Application.DTOs.SubordinateDto;
using ComplaintSystem.Application.DTOs.ComplaintDto;


namespace ComplaintSystem.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Profile
            CreateMap<UserEntity, CreateUserDto>().ReverseMap();
            CreateMap<UserEntity, GetUserDto>().ReverseMap();

            //Admin
            CreateMap<CreateAdminDto, Admin>().ReverseMap();

            //Manager
            CreateMap<CreateManagerDto, Manager>().ReverseMap();

            //Subordinate
            CreateMap<CreateSubordinateDto, Subordinate>().ReverseMap();

            //Complaint
            CreateMap<CreateComplaintDto, Complaint>().ReverseMap();

            //ComplaintLog
            CreateMap<CreateComplaintLogDto, ComplaintLog>().ReverseMap();
            CreateMap<UpdateComplaintLogStatusDto, ComplaintLog>().ReverseMap();


        }
    }
}