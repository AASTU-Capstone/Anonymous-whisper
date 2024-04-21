using ComplaintSystem.Application.DTOs.AdminDto;
using ComplaintSystem.Application.Responses;
using MediatR;

namespace ComplaintSystem.Application.Features.Admins.Requests.Commands
{
    public class CreateAdminRequest : IRequest<BaseResponseClass>
    {
        public CreateAdminDto CreateAdminDto { get; set; }

    }
}