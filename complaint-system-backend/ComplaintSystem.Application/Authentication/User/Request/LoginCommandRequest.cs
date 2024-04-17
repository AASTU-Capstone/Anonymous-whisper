using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  ComplaintSystem.Application.Authentication.common;
using MediatR;

namespace  ComplaintSystem.Application.Authentication.Request
{
    public record LoginCommandRequest(string Email, string Password) : IRequest<AuthenticationResult>;
}