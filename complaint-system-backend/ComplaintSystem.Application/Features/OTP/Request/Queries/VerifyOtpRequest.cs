using MediatR;
using  ComplaintSystem.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.Features.OTP.Request.Queries;
public class VerifyOtpRequest : IRequest<BaseResponseClass>
{
    public string OtpCode { get; set; }
    public string Email { get; set; }
}
