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
public class GetComplaintStatisticsRequestHandler : IRequestHandler<GetComplaintStatisticsRequest, BaseResponseClass>
{
    private readonly IComplaintRepository _complaintRepository;
    public GetComplaintStatisticsRequestHandler(IComplaintRepository complaintRepository)
    {
        _complaintRepository = complaintRepository;
    }
    public async Task<BaseResponseClass> Handle(GetComplaintStatisticsRequest request, CancellationToken cancellationToken)
    {
        var getcomplaintStatistics = await _complaintRepository.GetComplaintStatistics(request.UserId);
        BaseResponseClass response = new BaseResponseClass
        {
            Data = getcomplaintStatistics,
            StatusCode = 200,
            Success = true,
            Message = "Complaint Statistics Fetched Successfully"
        };

        return response;
    }
}
