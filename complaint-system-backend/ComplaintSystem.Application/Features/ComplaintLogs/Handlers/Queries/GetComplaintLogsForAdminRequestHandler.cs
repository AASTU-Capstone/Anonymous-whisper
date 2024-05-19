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
public class GetComplaintLogsForAdminRequestHandler : IRequestHandler<GetComplaintLogsForAdminRequest, PaginatedResponseClass>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly IMapper _mapper;
    private readonly IAdminRepository _adminRepository;

    public GetComplaintLogsForAdminRequestHandler(
        IComplaintLogRepository complaintLogRepository,
        IMapper mapper,
        IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
        _mapper = mapper;
        _complaintLogRepository = complaintLogRepository;
    }

    public async Task<PaginatedResponseClass> Handle(GetComplaintLogsForAdminRequest request, CancellationToken cancellationToken)
    {
        var admin = await _adminRepository.GetAsync(request.AdminId);

        PaginatedResponseClass response;
        if (admin != null)
        {
            var complaintsLog = await _complaintLogRepository.GetForAdmin(request.AdminId, request.Status, request.PaginationDto);
            var getComplaintLogs = _mapper.Map<List<GetComplaintLogsDto>>(complaintsLog);

            response = new PaginatedResponseClass
            {
                Success = true,
                StatusCode = 201,
                Data = getComplaintLogs,
                Message = "Complaint Log Fetched Successfully",
                TotalCount = await _complaintLogRepository.GetForAdminCount(request.AdminId, request.Status),
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
                Message = "Complaint Log Fetch Failed"
            };
        }

        return response;
    }
}
