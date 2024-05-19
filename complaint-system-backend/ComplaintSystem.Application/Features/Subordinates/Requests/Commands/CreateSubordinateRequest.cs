using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintSystem.Application.DTOs.SubordinateDto;
using ComplaintSystem.Application.Responses;
using MediatR;

namespace ComplaintSystem.Application.Features.Subordinates.Requests.Commands;
public class CreateSubordinateRequest : IRequest<BaseResponseClass>
{
    public CreateSubordinateControllerDto CreateSubordinateDto { get; set; }
    public Guid UserId { get; set; }

}