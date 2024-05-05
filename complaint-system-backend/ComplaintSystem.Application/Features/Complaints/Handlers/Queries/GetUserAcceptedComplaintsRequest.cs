using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Complaints.Handlers.Queries;

public class GetUserAcceptedComplaintsRequest : IRequest<BaseResponseClass>
{
    public Guid UserId { get; set; }
    public string Status {  get; set; }
}
