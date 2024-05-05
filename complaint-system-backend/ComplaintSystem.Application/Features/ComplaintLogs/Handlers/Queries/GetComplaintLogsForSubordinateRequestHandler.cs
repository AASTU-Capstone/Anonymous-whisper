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
public class GetComplaintLogsForSubordinateRequestHandler : IRequestHandler<GetComplaintLogsForSubordinateRequest, BaseResponseClass>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly ISubordinateRepository _subordinateRepository;
    private readonly IMapper _mapper;
    public GetComplaintLogsForSubordinateRequestHandler(IMapper mapper, IComplaintLogRepository complaintLogRepository, ISubordinateRepository subordinateRepository)
    {
        _complaintLogRepository = complaintLogRepository;
        _subordinateRepository = subordinateRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponseClass> Handle(GetComplaintLogsForSubordinateRequest request, CancellationToken cancellationToken)
    {
        var subordinate = await _subordinateRepository.GetAsync(request.SubordinateId);
        BaseResponseClass response;
        if(subordinate != null)
        {
            var complaintLogs = await _complaintLogRepository.GetForSubordinate(request.SubordinateId);
            var getComplaintLogs = _mapper.Map<GetComplaintLogsDto>(complaintLogs);

            response = new BaseResponseClass
            {
                StatusCode = 200,
                Success = true,
                Data = getComplaintLogs,
                Message = "Complaint Logs Fetched Successfully"
            };
        }
        else
        {
            response = new BaseResponseClass
            {
                StatusCode = 404,
                Success = false,
                Error = ["Subordinate is not found"],
                Message = "Complaint Logs Fetch Failed"
            };
        }

        return response;
    }
}
