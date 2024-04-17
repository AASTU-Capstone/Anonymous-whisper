using MediatR;
using  ComplaintSystem.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.Features.OTP.Request.Commands;

public class CreateOtpRequest : IRequest<BaseResponseClass>
{
    public string UserEmail {  get; set; }
}
