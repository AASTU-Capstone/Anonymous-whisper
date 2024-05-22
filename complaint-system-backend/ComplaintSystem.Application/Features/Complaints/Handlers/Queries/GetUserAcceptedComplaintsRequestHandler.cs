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

public class GetUserAcceptedComplaintsRequestHandler : IRequestHandler<GetUserAcceptedComplaintsRequest, PaginatedResponseClass>
{
    private readonly IMapper _mapper;
    private readonly IComplaintRepository _complaintRepository;
    private readonly IUserRepository _userRepository;
    public GetUserAcceptedComplaintsRequestHandler(IComplaintRepository complaintRepository, IMapper mapper, IUserRepository userRepository)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<PaginatedResponseClass> Handle(GetUserAcceptedComplaintsRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.UserId);
        PaginatedResponseClass response;
        if (user == null)
        {
            response = new PaginatedResponseClass
            {
                StatusCode = 404,
                Success = false,
                Error = ["User Does not exist"],
                Message = "Complaint Fetch Failed"
            };

        }
        else
        {
            var complaints = await _complaintRepository.GetUserComplaints(request.UserId, request.PaginationDto);
            var getComplaints = _mapper.Map<List<GetComplaintsDto>>(complaints);

            response = new PaginatedResponseClass
            {
                Data = getComplaints,
                StatusCode = 200,
                Success = true,
                Message = "Complaint Fetched Successfully",
                TotalCount = await _complaintRepository.GetUserAcceptedComplaintsCount(request.UserId),
                PageNumber = request.PaginationDto.PageNumber,
                PageSize = request.PaginationDto.PageSize
            };
        }

        return response;
    }
}