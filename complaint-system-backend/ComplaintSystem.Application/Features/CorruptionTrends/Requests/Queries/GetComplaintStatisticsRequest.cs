using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.CorruptionTrends.Requests.Queries;
public class GetComplaintStatisticsRequest : IRequest<BaseResponseClass>
{
    public Guid? UserId { get; set; }
}
