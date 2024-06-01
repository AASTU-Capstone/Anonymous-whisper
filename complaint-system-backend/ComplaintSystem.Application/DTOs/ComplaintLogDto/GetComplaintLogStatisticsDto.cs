using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintLogDto;
public class GetComplaintLogStatisticsDto : IRequest<BaseResponseClass>
{
    public int TotalComplaintLogs {  get; set; }
    public int PendingComplaintLogs { get; set; }
    public int ResolvedComplaintLogs { get; set; }
    public int AssignedComplaintLogs { get; set; }
}
