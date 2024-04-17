using MediatR;
using  ComplaintSystem.Application.DTOs.Authentication;
using  ComplaintSystem.Application.Responses;

namespace  ComplaintSystem.Application.Authentication.Request
{
    public class ChangePasswordRequest : IRequest<BaseResponseClass>
    {
        public ChangePasswordDto ChangePassword { get; set; }
        public Guid UserId { get; set; }
    }
}
