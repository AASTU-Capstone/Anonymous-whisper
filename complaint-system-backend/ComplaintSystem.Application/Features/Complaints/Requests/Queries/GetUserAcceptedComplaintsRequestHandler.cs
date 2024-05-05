using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintDto;
using ComplaintSystem.Application.Features.Complaints.Handlers.Queries;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Features.Complaints.Requests.Queries;

public class GetUserAcceptedComplaintsRequestHandler : IRequestHandler<GetUserAcceptedComplaintsRequest, BaseResponseClass>
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
    public async Task<BaseResponseClass> Handle(GetUserAcceptedComplaintsRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.UserId);
        BaseResponseClass response;
        if (user == null)
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
            var complaints = await _complaintRepository.GetUserComplaints(request.UserId, request.Status);
            var getComplaints = _mapper.Map<GetComplaintsDto>(complaints);

            response = new BaseResponseClass
            {
                Data = getComplaints,
                StatusCode = 200,
                Success = true,
                Message = "Complaint Fetched Successfully"
            };


        }

        return response;
    }
}
