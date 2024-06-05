using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Managers.Requests.Queries;
public class GetManagerProfileRequest : IRequest<BaseResponseClass>
{
    public Guid ManagerId { get; set; }
}
