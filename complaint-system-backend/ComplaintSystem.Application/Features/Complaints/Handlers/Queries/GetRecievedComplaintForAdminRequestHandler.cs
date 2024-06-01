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

public class GetRecievedComplaintForAdminRequestHandler : IRequestHandler<GetRecievedComplaintForAdminRequest, PaginatedResponseClass>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    public GetRecievedComplaintForAdminRequestHandler(IComplaintRepository complaintRepository, IMapper mapper)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
    }
    public async Task<PaginatedResponseClass> Handle(GetRecievedComplaintForAdminRequest request, CancellationToken cancellationToken)
    {
        var acceptedComplaints = await _complaintRepository.GetComplaintsForAdminByStatus(request.Status, request.PaginationDto);
        var getAcceptedComplaints = _mapper.Map<List<GetComplaintsDto>>(acceptedComplaints);

        PaginatedResponseClass response = new PaginatedResponseClass
        {
            StatusCode = 200,
            Success = true,
            Data = getAcceptedComplaints,
            Message = "Complaints Fetched Successfully",
            TotalCount = await _complaintRepository.GetComplaintsForAdminByStatusCount(request.Status),
            PageNumber = request.PaginationDto.PageNumber,
            PageSize = request.PaginationDto.PageSize
        };

        return response;
    }
}
