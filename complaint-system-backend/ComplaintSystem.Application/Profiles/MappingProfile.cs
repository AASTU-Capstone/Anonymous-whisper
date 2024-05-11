using AutoMapper;
using ComplaintSystem.Domain.Entities;
using ComplaintSystem.Application.DTOs.UserDto;
using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.DTOs.ManagerDto;
using ComplaintSystem.Application.DTOs.AdminDto;
using ComplaintSystem.Application.DTOs.SubordinateDto;
using ComplaintSystem.Application.DTOs.ComplaintDto;
using ComplaintSystem.Application.DTOs.ListingsDto;


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
            CreateMap<GetManagerDto, Manager>().ReverseMap();

            //Subordinate
            CreateMap<CreateSubordinateDto, Subordinate>().ReverseMap();
            CreateMap<GetSubordinateDto, Subordinate>().ReverseMap();

            //Complaint
            CreateMap<CreateComplaintDto, Complaint>().ReverseMap();
            CreateMap<GetComplaintsDto, Complaint>().ReverseMap();
            CreateMap<UpdateComplaintDto,Complaint>().ReverseMap();

            //ComplaintLog
            CreateMap<CreateComplaintLogDto, ComplaintLog>().ReverseMap();
            CreateMap<UpdateComplaintLogStatusDto, ComplaintLog>().ReverseMap();
            CreateMap<UpdateComplaintLogDto, ComplaintLog>().ReverseMap();
            CreateMap<GetComplaintLogsDto, ComplaintLog>().ReverseMap();

            //Listings 
            CreateMap<CreateListingsDto, ListingsEntity>().ReverseMap();
            CreateMap<UpdateListingsDto, ListingsEntity>().ReverseMap();
            CreateMap<GetListingsDto, ListingsEntity>().ReverseMap();

        }
    }
}