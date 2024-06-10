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
public class SearchComplaintLogRequestHandler : IRequestHandler<SearchComplaintLogRequest, PaginatedResponseClass>
{
    private readonly IComplaintLogRepository _complaintLogRepository;
    private readonly IMapper _mapper;
    public SearchComplaintLogRequestHandler(IComplaintLogRepository complaintLogRepository, IMapper mapper)
    {
        _complaintLogRepository = complaintLogRepository;
        _mapper = mapper;
    }
    public async Task<PaginatedResponseClass> Handle(SearchComplaintLogRequest request, CancellationToken cancellationToken)
    {
        var complaintLogs = await _complaintLogRepository.SearchComplaintLogs(request.Keyword, request.Status, request.Pagination);
        var totalCount = await _complaintLogRepository.GetSearchCountByStatus(request.Keyword,request.Status);
        var getComplaintLogs = _mapper.Map<List<GetComplaintLogsDto>>(complaintLogs);
        PaginatedResponseClass response = new PaginatedResponseClass
        {
            TotalCount = totalCount,
            Data = getComplaintLogs,
            StatusCode = 200,
            Success = true,
            PageSize = request.Pagination.PageSize,
            PageNumber = request.Pagination.PageNumber,
            Message = "Search Results Fetched Successfully",

        };

        return response;
    }
}
