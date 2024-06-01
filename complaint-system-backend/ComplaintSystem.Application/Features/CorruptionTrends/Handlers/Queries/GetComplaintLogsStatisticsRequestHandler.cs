using ComplaintSystem.Application.Features.CorruptionTrends.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.CorruptionTrends.Handlers.Queries;
public class GetComplaintLogsStatisticsRequestHandler : IRequestHandler<GetComplaintLogsStatisticsRequest, BaseResponseClass>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    public GetComplaintLogsStatisticsRequestHandler(IComplaintLogRepository complaintLogRepository)
    {
        _complaintLogRepository = complaintLogRepository;
    }
    public async Task<BaseResponseClass> Handle(GetComplaintLogsStatisticsRequest request, CancellationToken cancellationToken)
    {
        var complaintLogStatisticsDto = await _complaintLogRepository.GetComplaintLogStatistics(request.ManagerId, request.SubordinateId);
        BaseResponseClass response = new BaseResponseClass
        {
            Data = complaintLogStatisticsDto,
            StatusCode = 200,
            Success = true,
            Message = "Complaint Log Statistics Fetched Successfully"
        };
        return response;
    }
}
