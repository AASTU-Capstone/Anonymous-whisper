using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.Features.ComplaintLogs.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.ComplaintLogs.Handlers.Queries;
public class GetResolvedComplaintLogsRequestHandler : IRequestHandler<GetResolvedComplaintLogsRequest, BaseResponseClass>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly IMapper _mapper;
    public GetResolvedComplaintLogsRequestHandler(IMapper mapper, IComplaintLogRepository complaintLogRepository)
    {
        _complaintLogRepository = complaintLogRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(GetResolvedComplaintLogsRequest request, CancellationToken cancellationToken)
    {
        var complaints = await _complaintLogRepository.GetByStatus(request.Status);
        var getComplaints = _mapper.Map<List<GetComplaintLogsDto>>(complaints);
        BaseResponseClass response = new BaseResponseClass
        {
            Data = getComplaints,
            StatusCode = 200,
            Success = true,
            Message = "Resolved Complaint Logs Fetched"
        };

        return response;
    }
}
