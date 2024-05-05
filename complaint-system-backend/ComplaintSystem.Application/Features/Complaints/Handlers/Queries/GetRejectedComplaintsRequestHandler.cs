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

public class GetRejectedComplaintsRequestHandler : IRequestHandler<GetRejectedComplaintsRequest, BaseResponseClass>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetRejectedComplaintsRequestHandler(IComplaintRepository complaintRepository, IUserRepository userRepository, IMapper mapper)
    {
        _complaintRepository = complaintRepository;
        _userRepository = userRepository;
        _mapper = mapper;

    }
    public async Task<BaseResponseClass> Handle(GetRejectedComplaintsRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.UserId);
        BaseResponseClass response;
        
        if(user == null)
        {
            response = new BaseResponseClass
            {
                StatusCode = 404,
                Success = false,
                Error = ["User Does not exist"],
                Message = "Complaint Fetch Failed"
            };
        }
        else
        {
            var resolvedComplaints = await _complaintRepository.GetUserComplaints(request.UserId, request.Status);
            var complaints = _mapper.Map<List<GetComplaintsDto>>(resolvedComplaints);

            response = new BaseResponseClass
            {
                StatusCode = 200,
                Success = true,
                Data = complaints,
                Message = "Complaint Fetched Successfully"
            };
        }

        return response;
    }
}
