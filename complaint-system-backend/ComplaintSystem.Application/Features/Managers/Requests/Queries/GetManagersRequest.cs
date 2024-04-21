using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Managers.Requests.Queries;

public class GetManagersRequest : IRequest<BaseResponseClass>
{
    public Guid AdminId { get; set; }
}
