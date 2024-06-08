using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Resources.Request.Queries;
public class GetResourceByIdRequest : IRequest<BaseResponseClass>
{
    public Guid ResourceId { get; set; }
}
