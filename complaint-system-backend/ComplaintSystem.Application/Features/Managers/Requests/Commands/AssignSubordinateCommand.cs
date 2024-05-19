using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Managers.Requests.Commands;
public class AssignSubordinateCommand : IRequest<BaseResponseClass>
{
    public AssignSubordinateControllerDto ComplaintLog { get; set; }
    public Guid UserId { get; set; }
}
