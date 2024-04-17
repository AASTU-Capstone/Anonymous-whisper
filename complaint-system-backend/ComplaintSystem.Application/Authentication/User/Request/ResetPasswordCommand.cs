using MediatR;
using  ComplaintSystem.Application.DTOs.Authentication;
using  ComplaintSystem.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.Authentication.User.Request;

public class ResetPasswordCommand : IRequest<BaseResponseClass>
{
    public ResetPasswordDto ResetPassword { get; set; }
}
