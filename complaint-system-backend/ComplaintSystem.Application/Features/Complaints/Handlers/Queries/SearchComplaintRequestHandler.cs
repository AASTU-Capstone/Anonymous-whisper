using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintDto;
using ComplaintSystem.Application.Features.Complaints.Requests.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Complaints.Handlers.Queries;
public class SearchComplaintRequestHandler : IRequestHandler<SearchComplaintRequest, PaginatedResponseClass>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    public SearchComplaintRequestHandler(IMapper mapper, IComplaintRepository complaintRepository)
    {
        _mapper = mapper;
        _complaintRepository = complaintRepository;
    }
    public async Task<PaginatedResponseClass> Handle(SearchComplaintRequest request, CancellationToken cancellationToken)
    {
        var complaints = await _complaintRepository.GetMatchingComplaints(request.Keyword,request.Status, request.Category, request.DateOrder, request.PaginationDto);
        var getComplaints = _mapper.Map<List<GetComplaintsDto>>(complaints);

        PaginatedResponseClass response = new PaginatedResponseClass
        {
            Data = getComplaints,
            StatusCode = 200,
            Success = true,
            Message = "Search Results Fetched Successfully",
            TotalCount = await _complaintRepository.GetMatchingComplaintsCount(request.Keyword, request.Status,request.Category),
            PageNumber = request.PaginationDto.PageNumber,
            PageSize = request.PaginationDto.PageSize
        };

        return response;
    }
}
