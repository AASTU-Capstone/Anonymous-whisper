using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
public class GetComplaintLogsForAdminRequest : IRequest<BaseResponseClass>
{
    public Guid AdminId { get; set; }
    public string Status { get; set; }
}
