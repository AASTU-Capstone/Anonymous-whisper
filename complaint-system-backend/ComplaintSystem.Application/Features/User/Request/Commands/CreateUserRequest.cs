using   ComplaintSystem.Application.DTOs.UserDto;
using   ComplaintSystem.Application.Responses;
using MediatR;

namespace   ComplaintSystem.Application.Features.User.Request.Commands
{
    public class CreateUserRequest : IRequest<BaseResponseClass>
    {
        public CreateUserDto User { get; set; }
    }
}