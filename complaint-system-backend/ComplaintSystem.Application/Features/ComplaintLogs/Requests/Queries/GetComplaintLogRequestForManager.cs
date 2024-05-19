using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;

public class GetComplaintLogRequestForManager : IRequest<BaseResponseClass>
{
    public Guid ManagerId { get; set; }
    public string Status {  get; set; }
}
