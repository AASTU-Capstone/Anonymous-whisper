using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintSystem.Application.DTOs.ManagerDto;
using ComplaintSystem.Application.Responses;
using MediatR;

namespace ComplaintSystem.Application.Features.Managers.Requests.Commands
{
    public class CreateManagerRequest : IRequest<BaseResponseClass>
    {
        public CreateManagerDto CreateManagerDto { get; set; }
        public Guid AdminId { get; set; }
    }
}