using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Subordinates.Requests.Queries;

public class GetSubordinatesRequest : IRequest<BaseResponseClass>
{
    public Guid ManagerId { get; set; }
}
