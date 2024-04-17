using  ComplaintSystem.Application.Responses;
using MediatR;
using  ComplaintSystem.Application.DTOs.Authentication;    

namespace  ComplaintSystem.Application.Authentication.Request
{

    public class ForgetPasswordCommand : IRequest<BaseResponseClass>
    {
        public ForgetPasswordDto forgetPasswordDto { get; set; }
    }
}
