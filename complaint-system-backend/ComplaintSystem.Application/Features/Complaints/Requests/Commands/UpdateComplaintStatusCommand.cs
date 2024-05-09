using ComplaintSystem.Application.DTOs.ComplaintDto;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Complaints.Requests.Commands;
public class UpdateComplaintStatusCommand : IRequest<BaseResponseClass>
{
    public UpdateComplaintDto UpdateComplainDto { get; set; }
}
