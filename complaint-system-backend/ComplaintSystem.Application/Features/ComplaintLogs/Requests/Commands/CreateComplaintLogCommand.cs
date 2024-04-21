using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ComplaintLogs.Requests.Commands;
public class CreateComplaintLogCommand : IRequest<BaseResponseClass>
{
    public CreateComplaintLogDto ComplaintLogDto { get; set; }
}
