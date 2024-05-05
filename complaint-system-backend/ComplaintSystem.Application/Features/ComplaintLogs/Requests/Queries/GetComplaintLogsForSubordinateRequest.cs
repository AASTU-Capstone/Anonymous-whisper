using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
public class GetComplaintLogsForSubordinateRequest : IRequest<BaseResponseClass>
{
    public Guid SubordinateId { get; set; }
}
