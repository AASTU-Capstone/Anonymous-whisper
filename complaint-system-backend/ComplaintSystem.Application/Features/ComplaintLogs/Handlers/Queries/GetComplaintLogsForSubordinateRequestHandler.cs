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
public class GetComplaintLogsForSubordinateRequestHandler : IRequestHandler<GetComplaintLogsForSubordinateRequest, PaginatedResponseClass>
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
    public async Task<PaginatedResponseClass> Handle(GetComplaintLogsForSubordinateRequest request, CancellationToken cancellationToken)
    {
        var subordinate = await _subordinateRepository.GetSubordinateByUserId(request.UserId);
        PaginatedResponseClass response;
        if (subordinate != null)
        {
            var complaintLogs = await _complaintLogRepository.GetForSubordinate(subordinate.Id, request.Status, request.PaginationDto);
            var getComplaintLogs = _mapper.Map<List<GetComplaintLogsDto>>(complaintLogs);

            response = new PaginatedResponseClass
            {
                StatusCode = 200,
                Success = true,
                Data = getComplaintLogs,
                Message = "Complaint Logs Fetched Successfully",
                TotalCount = await _complaintLogRepository.GetForSubordinateCount(subordinate.Id, request.Status),
                PageNumber = request.PaginationDto.PageNumber,
                PageSize = request.PaginationDto.PageSize
            };
        }
        else
        {
            response = new PaginatedResponseClass
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
