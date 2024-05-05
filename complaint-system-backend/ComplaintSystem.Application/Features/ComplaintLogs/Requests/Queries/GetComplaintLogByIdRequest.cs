using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
public class GetComplaintLogByIdRequest : IRequest<BaseResponseClass>
{
    public Guid ComplaintLogId { get; set; }
}
