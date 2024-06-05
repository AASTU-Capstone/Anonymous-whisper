using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Admins.Requests.Queries;
public class GetAdminProfileRequest : IRequest<BaseResponseClass>
{
    public Guid AdminId { get; set; }
}
