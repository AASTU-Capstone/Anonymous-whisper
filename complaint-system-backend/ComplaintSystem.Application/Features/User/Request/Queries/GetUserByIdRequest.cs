using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using   ComplaintSystem.Application.DTOs.UserDto;
using   ComplaintSystem.Application.Responses;

namespace   ComplaintSystem.Application.Features.User.Request.Queries
{
    public class GetUserByIdRequest: IRequest<BaseResponseClass>
    {
        public Guid Id { get; set; }
    }
}