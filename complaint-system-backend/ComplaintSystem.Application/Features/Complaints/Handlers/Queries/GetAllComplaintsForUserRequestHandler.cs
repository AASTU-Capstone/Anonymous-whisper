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
public class GetAllComplaintsForUserRequestHandler : IRequestHandler<GetAllComplaintsForUserRequest, PaginatedResponseClass>
{
    private readonly IMapper _mapper;
    private readonly IComplaintRepository _complaintRepository;
    public GetAllComplaintsForUserRequestHandler(IComplaintRepository complaintRepository, IMapper mapper)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
    }
    public async Task<PaginatedResponseClass> Handle(GetAllComplaintsForUserRequest request, CancellationToken cancellationToken)
    {
        var complaints = await _complaintRepository.GetAllComplaintsForUser(request.UserId, request.PaginationDto);
        var totalCount = await _complaintRepository.GetAllUserComplaintsCount(request.UserId);
        var viewComplaints = _mapper.Map<List<ViewComplaintDto>>(complaints);
        PaginatedResponseClass response = new PaginatedResponseClass
        {
            Data = viewComplaints,
            StatusCode = 200,
            Success = true,
            Message = "All Complaints Fetched Successfully",
            TotalCount = totalCount,
            PageNumber = request.PaginationDto.PageNumber,
            PageSize = request.PaginationDto.PageSize
        };

        return response;
    }
}
