using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ComplaintLogs.Requests.Commands;
public class UpdateComplaintLogDtoCommand : IRequest<BaseResponseClass>
{
    public UpdateComplaintLogDto UpdateComplaintLogDto { get; set; }
    public Guid SubordinateId { get; set; }
}
