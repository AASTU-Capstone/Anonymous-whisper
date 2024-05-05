using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Managers.Requests.Commands;
public class UpdateComplaintLogStatusForManagerRequest : IRequest<BaseResponseClass>
{
    public UpdateComplaintLogStatusDto ComplaintLogStatus { get; set;}
    public Guid ManagerId { get; set;}
}
